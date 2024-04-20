using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/telefonos")]
    [ApiController]
    public class APITelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public APITelefonoController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var telefonos = _telefonoRepository.GetAll();
            return Ok(telefonos);
        }

        [HttpGet("{numero}")]
        public IActionResult GetByNumber(string numero)
        {
            var telefono = _telefonoRepository.GetByNumber(numero);
            if (telefono == null)
            {
                return NotFound();
            }
            return Ok(telefono);
        }

        [HttpPost]
        public IActionResult Create(Telefono telefono)
        {
            _telefonoRepository.Add(telefono);
            return CreatedAtAction(nameof(GetByNumber), new { numero = telefono.Num }, telefono);
        }

        [HttpPut("{numero}")]
        public IActionResult Update(string numero, Telefono telefono)
        {
            if (numero != telefono.Num)
            {
                return BadRequest();
            }
            _telefonoRepository.Update(telefono);
            return NoContent();
        }

        [HttpDelete("{numero}")]
        public IActionResult Delete(string numero)
        {
            _telefonoRepository.Delete(numero);
            return NoContent();
        }
    }
}
