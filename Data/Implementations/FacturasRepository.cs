using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInteriorV1.Data.Helper;
using ComercioInteriorV1.Data.Helpers;
using ComercioInteriorV1.Data.Interfaces;
using ComercioInteriorV1.Domain;
using ComercioInteriorV1.Services;

namespace ComercioInteriorV1.Data.Implementations
{
    public class FacturasRepository : IFacturaRepository
    {
        ArticuloService _articuloRepo ;

        private readonly DataHelper _dataHelper;

        public FacturasRepository(DataHelper dataHelper, ArticuloService articuloRepo)
        {
            _dataHelper = dataHelper;
            _articuloRepo = articuloRepo;
        }

        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
               new SpParameter()
               {
                   Name = "@id",
                   Valor = id
               }
            };

            return _dataHelper.ExecuteSpDml("sp_Eliminar_Factura", param);
        }

        public List<Facturas> GetAll()
        {
            var dt = _dataHelper.ExecuteSpQuery("sp_ObtenerFactura");
            List<Facturas> list = new List<Facturas>();

            foreach (DataRow l in dt.Rows)
            {
                Facturas a = new Facturas()
                {
                    Codigo = (int)l["id"],
                    Fecha = (DateTime)l["fecha"],
                    Pago = (int)l["pago"],
                    Cliente = (string)l["cliente"]
                };


                List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                    {
                        Name = "@idFactura",
                        Valor = a.Codigo
                    }
                };

                var dtDetalles = _dataHelper.ExecuteSpQuery("sp_ObtenerFacturasConDetalles", param);

                foreach (DataRow d in dtDetalles.Rows)
                {
                    int idArticulo = (int)d["NroArticulo"];
                    Articulos art = _articuloRepo.GetById(idArticulo);
                    DetalleFactura detalle = new DetalleFactura()
                    {
                        Codigo = (int)d["id"],
                        NroFacturaId = (int)d["NroFactura"],
                        NroArticuloId = (int)d["NroArticulo"],
                        Cantidad = (int)d["Cantidad"],
                        NroArticulo = art
                    };
                    a.detalleFacturas.Add(detalle);
                }

                var dtPago = _dataHelper.ExecuteSpQuery("sp_ObtenerPagoPorId", param);

                if(dtPago.Rows.Count> 0)
                {
                    a.Pago = (int)dtPago.Rows[0]["id"];
                    a.FormaPago = new FormaPago()
                    {
                        Codigo = (int)dtPago.Rows[0]["id"],
                        Nombre = (string)dtPago.Rows[0]["nombre"]
                    };
                }

                list.Add(a);

            }




            return list;


        }

        public Facturas? GetById(int id)
        {
            List<SpParameter> list = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@id",
                    Valor = id
                }
            };

            var dt = _dataHelper.ExecuteSpQuery("sp_ObtenerFactura_Por_Id", list);

            if (dt != null && dt.Rows.Count > 0)
            {
                Facturas p = new Facturas()
                {
                    Codigo = (int)dt.Rows[0]["id"],
                    Fecha = (DateTime)dt.Rows[0]["fecha"],
                    Pago = (int)dt.Rows[0]["pago"],
                    Cliente = (string)dt.Rows[0]["cliente"]
                };

                return p;
            }
            return null;
        }

        public bool Save(Facturas factura)
        {

            return _dataHelper.executeTransaction(factura);

        }

        public bool Update(Facturas factura)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
               new SpParameter()
               {
                   Name = "@id",
                   Valor = factura.Codigo
               },
               new SpParameter()
               {
                   Name = "@fecha",
                   Valor = factura.Fecha
               },
               new SpParameter()
               {
                   Name = "@pago",
                   Valor = factura.Pago
               },
               new SpParameter()
               {
                   Name = "@cliente",
                   Valor = factura.Cliente
               }
            };

            return _dataHelper.ExecuteSpDml("sp_Modificar_Factura", param);

        }


        public bool UpdateDetalle1(DetalleFactura df)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
               new SpParameter()
               {
                   Name = "@id",
                   Valor = df.Codigo
               },
               new SpParameter()
               {
                   Name = "@facturaId",
                   Valor = df.NroFacturaId
               },
               new SpParameter()
               {
                   Name = "@articuloId",
                   Valor = df.NroArticuloId
               },
               new SpParameter()
               {
                   Name = "@cantidad",
                   Valor = df.Cantidad
               }
            };

            return _dataHelper.ExecuteSpDml("sp_Modificar_DetalleFactura", param);

        }

        public bool UpdateDetalle(List<DetalleFactura> detalles)
        {
            bool allUpdated = true;

            foreach (var detalle in detalles)
            {
                bool updated = UpdateDetalle1(detalle); 
                if (!updated)
                    allUpdated = false;
            }

            return allUpdated;
        }
    }
}
