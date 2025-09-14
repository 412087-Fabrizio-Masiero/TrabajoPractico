using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInteriorV1.Data.Implementations;
using ComercioInteriorV1.Data.Interfaces;
using ComercioInteriorV1.Domain;

namespace ComercioInteriorV1.Services
{
    public class FacturaService
    {
        private IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;

        }
        public bool Delete(int id)
        {
            return _facturaRepository.Delete(id);
        }
        public Facturas? GetById(int id)
        {
            return _facturaRepository.GetById(id);
        }
        public bool Save(Facturas factura)
        {
            factura.detalleFacturas = factura.detalleFacturas
            .GroupBy(d => d.NroArticulo.Codigo)
            .Select(g => new DetalleFactura
                {
            NroArticulo = g.First().NroArticulo,
            Cantidad = g.Sum(x => x.Cantidad)
                }).ToList();
            return _facturaRepository.Save(factura);
        }

        public bool Update(Facturas facturas)
        {
            return _facturaRepository.Update(facturas);
        }

        public List<Facturas> GetAll()
        {
            return _facturaRepository.GetAll();
        }

    }
}
