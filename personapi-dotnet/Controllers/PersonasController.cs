using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Controllers.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
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
        public IActionResult Create(Persona persona)
        {
            _personaRepository.Add(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Persona persona)
        {
            if (id != persona.Cc)
            {
                return BadRequest();
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