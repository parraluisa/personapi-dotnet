using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers.api
{
    [Route("api/profesiones")]
    public class APIProfesionesController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IEstudioRepository _estudioRepository;

        public APIProfesionesController(IProfesionRepository profesionRepository, IEstudioRepository estudioRepository)
        {
            _profesionRepository = profesionRepository;
            _estudioRepository = estudioRepository;
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
            try
            {
                var profesion = _profesionRepository.GetById(id);
                if (profesion == null)
                {
                    return NotFound();
                }
            }catch (Exception ex)
            {

            }


            try
            {

                var estudios = _estudioRepository.GetAllByIdProf(id);

                if (estudios.Any())
                {
                    foreach (var estudio in estudios)
                    {
                        _estudioRepository.Delete(estudio.CcPer, estudio.IdProf);
                    }
                }

            } catch (Exception ex)
            {

            }


            _profesionRepository.Delete(id);

            return NoContent(); 
        }

    }
}
