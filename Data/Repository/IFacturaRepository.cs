using ComercioInteriorV2.Data.Models;

namespace ComercioInteriorV2.Data.Repository
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();

        Factura GetById(int id);

        bool Save(Factura factura);
        bool Update(Factura factura);
        bool Delete(int id);

    }
}
