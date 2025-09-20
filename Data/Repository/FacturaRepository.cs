using ComercioInteriorV2.Data.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;

namespace ComercioInteriorV2.Data.Repository
{
    public class FacturaRepository : IFacturaRepository
    {


        private ComercioInteriorDbContext _context;

        public FacturaRepository(ComercioInteriorDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var idE = GetById(id);
            if (idE != null)
            {
                _context.Facturas.Remove(idE);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Factura> GetAll()
        {
            return _context.Facturas.ToList();
        }

        public Factura GetById(int id)
        {
            return _context.Facturas.Find(id);
        }

        public bool Save(Factura factura)
        {
            if (factura != null) {
                _context.Facturas.Add(factura);
                _context.SaveChanges();
                return true;
            }
            
            return false;
        }

        public bool Update(Factura factura)
        {
            if (factura != null)
            {
                _context.Facturas.Update(factura);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }

    }