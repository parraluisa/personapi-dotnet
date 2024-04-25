using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{

    [Produces("application/json")]
    [Route("api/personas")]
    [ApiController]
    public class APIPersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public APIPersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var personas = _personaRepository.GetAll();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var persona = _personaRepository.GetById(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public IActionResult Create(int cc, string nombre, string apellido, string genero, int edad)
        {
            var persona = new Persona
            {
                Cc = cc,
                Nombre = nombre,
                Apellido = apellido,
                Genero = genero,
                Edad = edad,
            };
            _personaRepository.Add(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, string nombre = null, string apellido = null, string genero = null, int edad = 0)
        {
            var persona = _personaRepository.GetById(id);

            if (id != persona.Cc)
            {
                return BadRequest();
            }

            if (nombre != null)
            {
                persona.Nombre = nombre;
            }

            if (apellido != null)
            {
                persona.Apellido = apellido;
            }

            if (genero != null)
            {
                persona.Genero = genero;
            }

            if (edad != 0)
            {
                persona.Edad = edad;
            }

            _personaRepository.Update(persona);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personaRepository.Delete(id);
            return NoContent();
        }
    }
}