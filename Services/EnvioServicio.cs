using EnvioDeProductos.Models;
using EnvioDeProductos.Repository;

namespace EnvioDeProductos.Services
{
    public class EnvioServicio : IEnvioServicio
    {
        private readonly IEnviosRepository _enviosRepository;

        public EnvioServicio(IEnviosRepository enviosRepository)
        {
            _enviosRepository = enviosRepository;
        }


        public bool Delete(int id)
        {
            return _enviosRepository.Delete(id);
        }

        public List<Envio> GetAll()
        {
            return _enviosRepository.GetAll();
        }
        public List<Envio> GetAllEstado(string estado)
        {
            return _enviosRepository.GetAllEstado(estado);
        }
        public List<Envio> GetAllDirecion(string direccion)
        {
            return _enviosRepository.GetAllDireccion(direccion);
        }

        public List<Envio> GetAllDireccionOrEstado(string direccion, string estado)
        {
            return _enviosRepository.GetAllDireccionOrEstado(direccion, estado);
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
