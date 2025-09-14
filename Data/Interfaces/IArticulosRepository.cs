using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInteriorV1.Domain;

namespace ComercioInteriorV1.Data.Interfaces
{
    public interface IArticulosRepository
    {
        List<Articulos> GetAll();

        Articulos? GetById(int id);

        bool Save(Articulos articulo);
        bool Update(Articulos articulo);
        bool Delete(int id);
    }
}
