using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioInterior.Data.Implementations;
using ComercioInterior.Data.Interfaces;
using ComercioInterior.Domain;

namespace ComercioInterior.Services
{
    public class FacturaService
    {
        private IFacturaRepository _facturaRepository;

        public FacturaService()
        {
            _facturaRepository = new FacturasRepository();

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
            .GroupBy(d => d.NroArticulo)
            .Select(g => new DetalleFactura
                {
            NroArticulo = g.Key,
            Cantidad = g.Sum(x => x.Cantidad)
                }).ToList();
            return _facturaRepository.Save(factura);
        }

        public bool Update(Facturas facturas)
        {
            return _facturaRepository.Update(facturas);
        }

    }
}
