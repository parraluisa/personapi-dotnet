using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repositories;

namespace personapi_dotnet.Controllers.api
{

    [Produces("application/json")]
    [Route("api/personas")]
    [ApiController]
    public class APIPersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IEstudioRepository _estudioRepository;

        public APIPersonaController(IPersonaRepository personaRepository, ITelefonoRepository telefonoRepository, IEstudioRepository estudioRepository)
        {
            _personaRepository = personaRepository;
            _telefonoRepository = telefonoRepository;
            _estudioRepository = estudioRepository;
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

            var telefonos = _telefonoRepository.GetByDuenio(id);

            if (telefonos.Any())
            {
                List<string> phoneNumbersToDelete = telefonos.Select(t => t.Num).ToList();
                foreach (var phoneNumber in phoneNumbersToDelete)
                {
                    _telefonoRepository.Delete(phoneNumber);
                }

            }

            var estudios = _estudioRepository.GetAllByCcPer(id);
            if(estudios.Any())
            {
                List<int> studies = estudios.Select(t => t.CcPer).ToList();
                foreach (var estudio in estudios)
                {
                    _estudioRepository.Delete(estudio.CcPer, estudio.IdProf);
                }
            }

            _personaRepository.Delete(id);
            return NoContent();
        }
    }
}