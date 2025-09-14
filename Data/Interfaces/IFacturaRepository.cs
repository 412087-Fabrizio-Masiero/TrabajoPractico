using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInteriorV1.Domain;

namespace ComercioInteriorV1.Data.Interfaces
{
    public interface IFacturaRepository
    {
        List<Facturas> GetAll();

        Facturas GetById(int id);

        bool Save(Facturas factura);
        bool Update(Facturas factura);
        bool Delete(int id);
        
        bool UpdateDetalle1(DetalleFactura detalleFactura);
        bool UpdateDetalle(List<DetalleFactura> detalles);

    }
}
