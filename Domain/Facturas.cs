using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioInterior.Domain
{
    public class Facturas
    {
        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public int Pago { get; set; }

        public string Cliente { get; set; }

        public List<DetalleFactura> detalleFacturas { get; set; } = new List<DetalleFactura>();
    }
}
