using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/estudios")]
    [ApiController]
    public class APIEstudiosController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IProfesionRepository _profesionRepository;
        private readonly IPersonaRepository _personaRepository;

        public APIEstudiosController(IEstudioRepository estudioRepository, IProfesionRepository profesionRepository, IPersonaRepository personaRepository)
        {
            _estudioRepository = estudioRepository;
            _profesionRepository = profesionRepository;
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var estudios = _estudioRepository.GetAll();
            return Ok(estudios);
        }
        [HttpGet("{ccPer}/{idProf}")]
        public IActionResult GetById(int ccPer, int idProf)
        {
            var estudio = _estudioRepository.GetById(ccPer, idProf);
            if (estudio == null)
            {
                return NotFound();
            }
            return Ok(estudio);
        }

        [HttpPost]
        public IActionResult Create(int id_profesion, int cc_persona, DateOnly date, string universidad)
        {
            // Verificar si el estudio ya existe
            var existingEstudio = _estudioRepository.GetById(cc_persona, id_profesion);

            if (existingEstudio != null)
            {
                return Conflict("El estudio ya existe para esta persona y profesión.");
            }

            // Crear un nuevo objeto Estudio
            var estudio = new Estudio
            {
                IdProf = id_profesion,
                CcPer = cc_persona,
                Fecha = date,
                Univer = universidad
            };

            // Agregar el estudio a la lista de estudios de la persona
            var persona = _personaRepository.GetById(cc_persona);
            persona.Estudios.Add(estudio);
            _personaRepository.Update(persona);

            // Agregar el estudio a la lista de estudios de la profesion
            var profesion = _profesionRepository.GetById(id_profesion);
            profesion.Estudios.Add(estudio);
            _profesionRepository.Update(profesion);


            // Configurar las opciones de serialización para evitar ciclos de referencias
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serializar el estudio con las opciones configuradas
            var serializedEstudio = JsonSerializer.Serialize(estudio, options);

            // Devolver una respuesta exitosa con el estudio creado
            return CreatedAtAction(nameof(GetById), new { ccPer = estudio.CcPer, idProf = estudio.IdProf }, serializedEstudio);
        }


        [HttpPut("{ccPer}/{idProf}")]
        public IActionResult Update(int ccPer, int idProf, string universidad, DateOnly date)
        {
            // Obtener el estudio a actualizar
            var estudio = _estudioRepository.GetById(ccPer, idProf);

            // Verificar si el estudio existe
            if (estudio == null)
            {
                return NotFound();
            }

            // Actualizar los campos de universidad si no están vacíos
            if (!string.IsNullOrEmpty(universidad))
            {
                estudio.Univer = universidad;
            }

            // Actualizar el campo de fecha si no está vacío
            if (date != default)
            {
                estudio.Fecha = date;
            }

            // Actualizar el estudio en el repositorio
            _estudioRepository.Update(estudio);

            return NoContent();
        }



        [HttpDelete("{ccPer}/{idProf}")]
        public IActionResult Delete(int ccPer, int idProf)
        {
            _estudioRepository.Delete(ccPer, idProf);
            return NoContent();
        }
    }
}
