using EnvioDeProductos.Models;

namespace EnvioDeProductos.Repository
{
    public interface IEnviosRepository
    {

        List<Envio>GetAll();
        List<Envio> GetAllEstado(string estado);
        List<Envio> GetAllDireccion(string direccion);
        List<Envio> GetAllDireccionOrEstado(string direccion, string estado);
        bool Save(Envio envio);

        bool Update(Envio envio);
        bool Delete(int id);


    }
}
