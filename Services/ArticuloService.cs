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
    public class ArticuloService
    {
        private IArticulosRepository _articulosRepository;
      
        public ArticuloService() { 
            _articulosRepository = new ArticulosRepository();

        }
        public List<Articulos> GetAll()
        {
            return _articulosRepository.GetAll();
        }

        public bool Delete(int id)
        {
            return _articulosRepository.Delete(id);
        }
        public Articulos? GetById(int id)
        {
            return _articulosRepository.GetById(id);
        }
        public bool Save(Articulos articulo)
        {


            return _articulosRepository.Save(articulo);
        }

        public bool Update(Articulos articulo)
        {
            return _articulosRepository.Update(articulo);
        }
    }
}
