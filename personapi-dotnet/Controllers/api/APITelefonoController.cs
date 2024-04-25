using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repositories;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/telefonos")]
    [ApiController]
    public class APITelefonoController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        private readonly IPersonaRepository _personaRepository;

        public APITelefonoController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
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
        public IActionResult Create(string numero, string operador, int duenioCedula)
        {
            // Verificar si la persona con la cédula proporcionada existe
            var persona = _personaRepository.GetById(duenioCedula);
            if (persona == null)
            {
                return BadRequest("La persona con la cédula proporcionada no existe.");
            }

            // Crear un nuevo objeto de teléfono y asignar los valores
            var telefono = new Telefono
            {
                Num = numero,
                Oper = operador,
                Duenio = duenioCedula
            };

            // Agregar el teléfono a la persona correspondiente
            persona.Telefonos.Add(telefono);
            _personaRepository.Update(persona);

            // Configurar las opciones de serialización para evitar ciclos de referencias
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serializar el teléfono con las opciones configuradas
            var serializedTelefono = JsonSerializer.Serialize(telefono, options);

            return CreatedAtAction(nameof(GetByNumber), new { numero = telefono.Num }, serializedTelefono);
        }


        [HttpPut("{numero}")]
        public IActionResult Update(string numero, string oper)
        {
            // Obtener el teléfono existente de la base de datos
            var existingTelefono = _telefonoRepository.GetByNumber(numero);
            if (existingTelefono == null)
            {
                return NotFound();
            }

            // Actualizar el valor de 'oper' del teléfono existente con el valor recibido en la solicitud
            existingTelefono.Oper = oper;

            // Guardar los cambios en la base de datos
            _telefonoRepository.Update(existingTelefono);

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
