using EnvioDeProductos.Models;
using EnvioDeProductos.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvioDeProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private readonly IEnvioServicio _envioServicio;

        public EnvioController(IEnvioServicio envioServicio)
        {
            _envioServicio = envioServicio;   
        }





        // GET: api/<EnvioController>
        //[HttpGet]
        //[Route("Envios")]
        //public IActionResult Get()
        //{
        //    try {

        //        var enviolist = _envioServicio.GetAll();
        //        if (enviolist == null)
        //        {
        //            return NotFound("No se encontro lista");

        //        }
        //        else
        //        {
        //            return Ok(enviolist);
        //        }
        //    }
        //    catch (Exception) {
        //        return StatusCode(500, "Error Interno");
        //    }
        //}
        [HttpGet]
        [Route("Envios/Direccion&estado")]

        public IActionResult GetDirecionOrEstado([FromQuery]string? direcion,[FromQuery] string? estado) {
            try
            {
                List<Envio> lista;
                if (string.IsNullOrEmpty(direcion) && string.IsNullOrEmpty(estado))
                {

                    lista = _envioServicio.GetAll();

                }
                else if (string.IsNullOrEmpty(direcion))
                {
                    lista =_envioServicio.GetAllEstado(estado);
                }
                else if (string.IsNullOrEmpty(estado))
                {
                    lista = _envioServicio.GetAllDirecion(direcion);

                }
                else
                {
                    lista =_envioServicio.GetAllDireccionOrEstado(direcion, estado);
                }

                if(lista == null)
                {
                    return NotFound("No se encontraron envios");
                }
                return Ok(lista); 
            }
            catch (Exception) {
                return StatusCode(500, "Error interno");
            }
        }

        // Envio solo por estado
        //[HttpGet]
        //[Route("Envios/Estado")]

        //public IActionResult GetEstado([FromQuery]string estado)
        //{
        //    try
        //    {

        //        var envioEstado = _envioServicio.GetAllEstado(estado);
        //        if (estado == null)
        //        {
        //            return BadRequest("No Puede estar vacio el estado");

        //        }
        //        else
        //        {
        //            if (envioEstado == null)
        //            {
        //                return NotFound("No se encontro lista");
        //            }
        //            return Ok(envioEstado);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Error Interno");
        //    }
            
        //}
        // envio solo por direccion
        //[HttpGet]
        //[Route("Envios/direccion")]

        //public IActionResult GetDireccion([FromQuery] string direccion)
        //{
        //    try
        //    {

        //        var envioDireccion = _envioServicio.GetAllDirecion(direccion);
        //        if (direccion == null)
        //        {
        //            return BadRequest("No Puede estar vacio el estado");

        //        }
        //        else
        //        {
        //            if (envioDireccion == null)
        //            {
        //                return NotFound("No se encontro lista");
        //            }
        //            return Ok(envioDireccion);

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Error Interno");
        //    }

        //}

        // POST api/<EnvioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EnvioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EnvioController>/5
        [HttpDelete]
        [Route("Cancelar")]
        public IActionResult Delete([FromQuery]int id)
        {
            try
            {
                var envioEliminado = _envioServicio.Delete(id);


                if (id <0)
                {
                    return BadRequest("No Puede ser 0 o negativo");

                }
                else
                {
                    if (!envioEliminado)
                    {
                        return BadRequest("No puede ser cancelado");
                    }
                    else { 
                        return Ok("Envio Cancelado");

                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error Interno");
            }
        }
    }
}
