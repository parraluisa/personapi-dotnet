using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/profesiones")]
    public class APIProfesionesController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;
        public APIProfesionesController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var profesiones = _profesionRepository.GetAll();
            return Ok(profesiones);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var profesion = _profesionRepository.GetById(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return Ok(profesion);
        }

        [HttpPost]
        public IActionResult Create(Profesion profesion)
        {
            _profesionRepository.Add(profesion);
            return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, string nombre, string descripcion)
        {
            // Obtener la profesión existente de la base de datos
            var profesion = _profesionRepository.GetById(id);
            if (profesion == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades de la profesión existente
            if(nombre != null)
            {
                profesion.Nom = nombre;
            }

            if(descripcion != null)
            {
                profesion.Des = descripcion;
            }

            // Guardar los cambios en la base de datos
            _profesionRepository.Update(profesion);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _profesionRepository.Delete(id);
            return NoContent();
        }

    }
}
