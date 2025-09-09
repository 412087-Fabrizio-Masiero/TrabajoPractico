using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ComercioInterior.Domain
{
    public class Facturas
    {
        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public int Pago { get; set; }

        public FormaPago FormaPago { get; set; }

        public string Cliente { get; set; }

        public List<DetalleFactura> detalleFacturas { get; set; } = new List<DetalleFactura>();
        public override string ToString()
        {
            string detalles = string.Join(" | ", detalleFacturas);
            return "La factura  " + Codigo + " es del dia " + Fecha+", del cliente "+Cliente +" "+ detalles +" y "+ FormaPago;
        }

    }
}
