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

namespace ComercioInterior.Data.Implementations
{
    public class ArticulosRepository : IArticulosRepository
    {


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

            return DataHelper.GetInstance().ExecuteSpDml("sp_Eliminar_Articulo", param);
        }

        public List<Articulos> GetAll()
        {
            var dt = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerArticulos");
            List<Articulos> list = new List<Articulos>();

            foreach (DataRow l in dt.Rows) {
                Articulos a = new Articulos()
                {
                    Codigo = (int)l["id"],
                    Nombre = (string)l["nombre"],
                    PrecioUnitario = (decimal)l["precioUnitario"]
                };

                
                list.Add(a);

            }

            return list;


        }

        public Articulos? GetById(int id)
        {
            List<SpParameter> list = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@id",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSpQuery("sp_ObtenerArticulos_Por_Id", list);

            if (dt != null && dt.Rows.Count > 0)
            {
                Articulos p = new Articulos()
                {
                    Codigo = (int)dt.Rows[0]["id"],
                    Nombre = (string)dt.Rows[0]["nombre"],
                    PrecioUnitario = (decimal)dt.Rows[0]["precioUnitario"]
                };

                return p;
            }
            return null;
        }

        public bool Save(Articulos articulo)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
               new SpParameter()
               {
                   Name = "@id",
                   Valor = articulo.Codigo
               },
               new SpParameter()
               {
                   Name = "@nombre",
                   Valor = articulo.Nombre
               },
               new SpParameter()
               {
                   Name = "@precioUnitario",
                   Valor = articulo.PrecioUnitario
               }
            };

            return DataHelper.GetInstance().ExecuteSpDml("sp_Guardar_Articulo", param);

        }

        public bool Update(Articulos articulo)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
               new SpParameter()
               {
                   Name = "@id",
                   Valor = articulo.Codigo
               },
               new SpParameter()
               {
                   Name = "@nombre",
                   Valor = articulo.Nombre
               },
               new SpParameter()
               {
                   Name = "@precioUnitario",
                   Valor = articulo.PrecioUnitario
               }
            };

            return DataHelper.GetInstance().ExecuteSpDml("sp_Modificar_Articulo", param);

        }
    }
}
