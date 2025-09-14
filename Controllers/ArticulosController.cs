using ComercioInteriorV1.Data.Interfaces;
using ComercioInteriorV1.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComercioInteriorV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        IArticulosRepository _articuloService;
        public ArticulosController(IArticulosRepository articuloService) {
            _articuloService = articuloService;

        }
        //Listara ARticulos
        [HttpGet]
        [Route("Articulos")]
        public IActionResult Index() {
            try
            {
                return Ok(_articuloService.GetAll());

            }
            catch (Exception ) {
                return StatusCode(500, "error Interno");
            }

        }

        //Listar ARticulo por id    


        [HttpGet]
        [Route("Articulos/{id}")]
        public IActionResult GetID(int id)
        {
            try
            {
                return Ok(_articuloService.GetById(id));

            }
            catch (Exception)
            {
                return StatusCode(500, "error Interno");
            }

        }
        //Actualizar por id

        [HttpPut]
        [Route("Actualizacion/Articulo/{id}")]
        public IActionResult Update(int id, [FromBody] Articulos articulo) {
            try
            {

                if (articulo != null)
                {
                    var newArticulo = _articuloService.GetById(id);
                    
                    if(newArticulo != null)
                    {
   
                        newArticulo.Nombre = articulo.Nombre;
                        newArticulo.PrecioUnitario = articulo.PrecioUnitario;
                        _articuloService.Update(newArticulo);
                        return Ok("se actualizo un Articulo");
                    }
                    


                }
                return StatusCode(500, "Error, no puede ser null");



            }
            catch (Exception) {
                return StatusCode(500, "Eror al actualizar");
            }
        }

        // eliminar 
        [HttpPost]
        [Route("Create/Articulo")]
        public IActionResult Create([FromBody] Articulos articulo)
        {
            try
            {
                if(articulo != null)
                {
                    return Ok(_articuloService.Save(articulo));
                }
                return StatusCode(500, "No puede ser null");
            }
            catch (Exception) {
                return StatusCode(500, "No se creo un articulo");
            }

        }



        [HttpDelete]
        [Route("Delete/Articulo/{id}")]

        public IActionResult Delete(int id) {
            try
            {
                if (id > 0 && id != null)
                {
                    return Ok(_articuloService.Delete(id));

                }

                return StatusCode(500, "no se pude eliminar un articulo null o con un id negativo");

            }
            catch (Exception) { 
                return StatusCode(500, "no se pude eliminar un articulo");

            }
        }
    }
}
