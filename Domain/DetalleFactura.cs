using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioInteriorV1.Domain
{
    public class DetalleFactura
    {
        public int Codigo { get; set; }
        public int NroFacturaId { get; set; }
        public int NroArticuloId { get; set; }
        
        public Articulos NroArticulo { get; set; }

        public int Cantidad { get; set; }
        public override string ToString()
        {
            return "Artículo " + NroArticulo.Nombre + " x " + Cantidad;
        }
    }
}
