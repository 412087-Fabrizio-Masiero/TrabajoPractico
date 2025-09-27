using EnvioDeProductos.Models;

namespace EnvioDeProductos.Services
{
    public interface IEnvioServicio
    {

        List<Envio> GetAll();
        List<Envio> GetAllEstado(string estado);
        List<Envio> GetAllDirecion(string direccion);
        List<Envio> GetAllDireccionOrEstado(string direccion, string estado);
        bool Save(Envio envio);

        bool Update(Envio envio);
        bool Delete(int id);

    }
}
