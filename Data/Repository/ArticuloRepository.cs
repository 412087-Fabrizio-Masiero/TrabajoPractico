using ComercioInteriorV2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComercioInteriorV2.Data.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {

        private ComercioInteriorDbContext _context;

        public ArticuloRepository(ComercioInteriorDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var idE = GetById(id);
            if (idE != null)
            {
                _context.Articulos.Remove(idE);
                _context.SaveChanges();
                return true;
            }
            return false;
            
        }

        public List<Articulo> GetAll()
        {
            return _context.Articulos.ToList();
        }

        public Articulo? GetById(int id)
        {
            return _context.Articulos.Find(id);
        }

        public bool Save(Articulo articulo)
        {
            if (articulo != null)
            {
                _context.Articulos.Add(articulo);
                _context.SaveChanges();
                return true;
            }

            return false;
            
        }

        public bool Update(Articulo articulo)
        {
            if (articulo != null)
            {
                _context.Articulos.Update(articulo);
                _context.SaveChanges();
                return true;
            }

            return false;
            
        }
    }
}
