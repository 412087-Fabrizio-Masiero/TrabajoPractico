using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioInterior.Domain
{
    public class DetalleFactura
    {
        public int Codigo { get; set; }

        public Facturas NroFactura { get; set; }
        public Articulos NroArticulo { get; set; }

        public int Cantidad { get; set; }
    }
}
