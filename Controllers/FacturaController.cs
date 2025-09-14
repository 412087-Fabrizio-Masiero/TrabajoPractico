using ComercioInteriorV1.Data.Interfaces;
using ComercioInteriorV1.Domain;
using ComercioInteriorV1.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComercioInteriorV1.Controllers
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
        [Route("/Facturas")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_facturaRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "No hay nada que mostrar");
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet]
        [Route("Facttura/{id}")]
        public IActionResult GetID(int id)
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
        public IActionResult Create([FromBody] Facturas facturas)
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
        public IActionResult Update(int id, [FromBody] Facturas factura)
        {
            try
            {

                if (factura != null)
                {
                    var newFactura = _facturaRepository.GetById(id);

                    if (newFactura != null)
                    {
                        newFactura.Fecha = factura.Fecha;
                        newFactura.FormaPago = factura.FormaPago;
                        newFactura.Pago = factura.Pago;
                        newFactura.FormaPago = factura.FormaPago;
                        newFactura.Cliente = factura.Cliente;
                        
                        _facturaRepository.Update(newFactura);
                        _facturaRepository.UpdateDetalle(factura.detalleFacturas);



                        return Ok("se actualizo un Articulo");
                    }
                    return StatusCode(500, "Error, no puede ser null");



                }
                return StatusCode(500, "Error, no puede ser null");



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
                if (id > 0 && id != null)
                {
                    return Ok(_facturaRepository.Delete(id));

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

