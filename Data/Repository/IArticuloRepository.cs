using ComercioInteriorV2.Data.Models;

namespace ComercioInteriorV2.Data.Repository
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo? GetById(int id);

        bool Save(Articulo articulo);
        bool Update(Articulo articulo);
        bool Delete(int id);
    }
}
