using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInterior.Domain;

namespace ComercioInterior.Data.Interfaces
{
    public interface IArticulosRepository
    {
        List<Articulos> GetAll();

        Articulos GetById(int id);

        bool Save(Articulos articulo);
        bool Update(Articulos articulo);
        bool Delete(int id);
    }
}
