using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace personapi_dotnet.Controllers
{
    [Route("api/estudios")]
    [ApiController]
    public class APIEstudiosController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;

        public APIEstudiosController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
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
        public IActionResult Create(Estudio estudio)
        {
            _estudioRepository.Add(estudio);
            return CreatedAtAction(nameof(GetById), new { ccPer = estudio.CcPer, idProf = estudio.IdProf }, estudio);
        }

        [HttpPut("{ccPer}/{idProf}")]
        public IActionResult Update(int ccPer, int idProf, Estudio estudio)
        {
            if (ccPer != estudio.CcPer || idProf != estudio.IdProf)
            {
                return BadRequest();
            }
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
