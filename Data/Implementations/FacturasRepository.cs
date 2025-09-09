using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInterior.Data.Helper;
using ComercioInterior.Data.Helpers;
using ComercioInterior.Data.Interfaces;
using ComercioInterior.Domain;
using ComercioInterior.Services;

namespace ComercioInterior.Data.Implementations
{
    public class FacturasRepository : IFacturaRepository
    {
        ArticuloService articuloRepo = new ArticuloService();

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

            return DataHelper.GetInstance().ExecuteSpDml("sp_Eliminar_Factura", param);
        }

        public List<Facturas> GetAll()
        {
            var dt = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerFactura");
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

                var dtDetalles = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerFacturasConDetalles", param);

                foreach (DataRow d in dtDetalles.Rows)
                {
                    int idArticulo = (int)d["NroArticulo"];
                    Articulos art = articuloRepo.GetById(idArticulo);
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

                var dtPago = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerPagoPorId", param);

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

            var dt = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerFactura_Por_Id", list);

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

            return DataHelper.GetInstance().executeTransaction(factura);

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

            return DataHelper.GetInstance().ExecuteSpDml("sp_Modificar_Factura", param);

        }
    }
}
