using EnvioDeProductos.Models;

namespace EnvioDeProductos.Repository
{
    public class EnviosRepository : IEnviosRepository
    {

        private readonly GestionEnviosContext _context;

        public EnviosRepository(GestionEnviosContext context)
        {
            _context = context;
        }
        public List<Envio> GetAll()
        {
            return _context.Envios.ToList();
        }
        public bool Delete(int id)
        {
            var envioEliminado = _context.Envios.Find(id);
            if (envioEliminado != null) {
                envioEliminado.Estado = "Cancelado";
                _context.Update(envioEliminado);
                return _context.SaveChanges()>0;

            }
            return false;
        }


        public List<Envio> GetAllEstado(string estado)
        {
            return _context.Envios.Where(x => x.Estado.Contains(estado)).ToList();
        }
        public List<Envio> GetAllDireccion(string direccion)
        {
            return _context.Envios.Where(x => x.Direccion.Contains(direccion)).ToList();
        }

        public List<Envio> GetAllDireccionOrEstado(string direccion, string estado)
        {
            return _context.Envios.Where(x => x.Direccion.Contains(direccion) && x.Estado.Contains(estado)).ToList();
        }

        public bool Save(Envio envio)
        {
            throw new NotImplementedException();
        }

        public bool Update(Envio envio)
        {
            throw new NotImplementedException();
        }
    }
}
