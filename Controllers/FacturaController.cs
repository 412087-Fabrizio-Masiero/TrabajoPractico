using ComercioInteriorV2.Data.Models;
using ComercioInteriorV2.Data.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioInteriorV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaRepository _facturaRepository;

        public FacturaController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }


        // GET: api/<FacturaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_facturaRepository.GetAll());

            }
            catch (Exception)
            {
                return StatusCode(500, "error Interno");
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public IActionResult GetID([FromHeader] int id)
        {
            try
            {
                return Ok(_facturaRepository.GetById(id));

            }
            catch (Exception)
            {
                return StatusCode(500, "error Interno");
            }
        }

        // POST api/<FacturaController>
        [HttpPost]
        [Route("Factura")]
        public IActionResult Create([FromBody] Factura facturas)
        {
            try
            {
                if (facturas != null)
                {
                    return Ok(_facturaRepository.Save(facturas));
                }
                return StatusCode(500, "No puede ser null");
            }
            catch (Exception)
            {
                return StatusCode(500, "No se creo un articulo");
            }

        }

        // PUT api/<FacturaController>/5
        [HttpPut]
        [Route("Actualizacion/Factura/{id}")]
        public IActionResult Update(int id, [FromBody] Factura factura)
        {
            try
            {

                if (factura != null)
                {
                    var newFactura = _facturaRepository.GetById(id);

                    if (newFactura != null)
                    {
                        newFactura.Fecha = factura.Fecha;
                        newFactura.PagoNavigation.Id = factura.PagoNavigation.Id;
                        newFactura.Pago = factura.Pago;
                        newFactura.Cliente = factura.Cliente;

                        _facturaRepository.Update(newFactura);

                        return Ok("se actualizo un Articulo");
                    }
                    return BadRequest("Error, no puede ser null");



                }
                return NotFound("Error, no puede ser null");



            }
            catch (Exception)
            {
                return StatusCode(500, "Eror al actualizar");
            }
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete]
        [Route("Delete/Factura/{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0 )
                {

                    return _facturaRepository.Delete(id) ? Ok("Fue Eliminado con exito"): BadRequest("No se elimino") ;


                }

                return BadRequest( "no se pude eliminar un articulo null o con un id negativo");

            }
            catch (Exception)
            {
                return StatusCode(500, "no se pude eliminar un articulo");

            }
        }
    }
}
