using ComercioInteriorV2.Data.Models;
using ComercioInteriorV2.Data.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioInteriorV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private IArticuloRepository _articuloRepository;

        public ArticuloController(IArticuloRepository articuloRepository)
        {
            _articuloRepository = articuloRepository;
        }



        // GET: api/<ArticuloController>
        [HttpGet]
        [Route("Articulos")]
        public IActionResult get()
        {
            try
            {
                return Ok(_articuloRepository.GetAll());

            }
            catch (Exception)
            {
                return StatusCode(500, "error Interno");
            }

        }

        // GET api/<ArticuloController>/5
        [HttpGet]
        [Route("Articulos/{id}")]
        public IActionResult GetID(int id)
        {
            try
            {
                return Ok(_articuloRepository.GetById(id));

            }
            catch (Exception)
            {
                return StatusCode(500, "error Interno");
            }

        }

        // POST api/<ArticuloController>
        [HttpPut]
        [Route("Actualizacion/Articulo/{id}")]
        public IActionResult Update(int id, [FromBody] Articulo articulo)
        {
            try
            {

                if (articulo != null)
                {
                    var newArticulo = _articuloRepository.GetById(id);

                    if (newArticulo != null)
                    {

                        newArticulo.Nombre = articulo.Nombre;
                        newArticulo.PrecioUnitario = articulo.PrecioUnitario;
                        _articuloRepository.Update(newArticulo);
                        return Ok("se actualizo un Articulo");
                    }



                }
                return BadRequest( "Error, no puede ser null");



            }
            catch (Exception)
            {
                return StatusCode(500, "Eror al actualizar");
            }
        }

        // PUT api/<ArticuloController>/5
        [HttpPost]
        [Route("Create/Articulo")]
        public IActionResult Create([FromBody] Articulo articulo)
        {
            try
            {
                if (articulo != null)
                {
                    return Ok(_articuloRepository.Save(articulo));
                }
                return StatusCode(500, "No puede ser null");
            }
            catch (Exception)
            {
                return StatusCode(500, "No se creo un articulo");
            }

        }

        // DELETE api/<ArticuloController>/5
        [HttpDelete]
        [Route("Delete/Articulo/{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0 )
                {
                    return Ok(_articuloRepository.Delete(id));

                }

                return StatusCode(500, "no se pude eliminar un articulo null o con un id negativo");

            }
            catch (Exception)
            {
                return StatusCode(500, "no se pude eliminar un articulo");

            }
        }
    }
}
