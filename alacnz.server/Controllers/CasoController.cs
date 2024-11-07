using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CasoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCasos()
        {
            var casos = await _unitOfWork.Casos.GetAllAsync();
            return Ok(casos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCasoById(int id)
        {
            var caso = await _unitOfWork.Casos.GetByIdAsync(id);
            if (caso == null)
                return NotFound();

            return Ok(caso);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCaso(Caso caso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Casos.AddAsync(caso);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetCasoById), new { id = caso.Id }, caso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCaso(int id, Caso updatedCaso)
        {
            if (id != updatedCaso.Id || !ModelState.IsValid)
                return BadRequest();

            var caso = await _unitOfWork.Casos.GetByIdAsync(id);
            if (caso == null)
                return NotFound();

            caso.FechaCaso = updatedCaso.FechaCaso;
            caso.ClienteId = updatedCaso.ClienteId;
            caso.NumeroBeneficiariosAdultos = updatedCaso.NumeroBeneficiariosAdultos;
            caso.NumeroBeneficiariosNinos = updatedCaso.NumeroBeneficiariosNinos;
            caso.NumeroBeneficiariosJovenes = updatedCaso.NumeroBeneficiariosJovenes;
            caso.EquipoTrabajo = updatedCaso.EquipoTrabajo;
            caso.CiudadServicio = updatedCaso.CiudadServicio;
            caso.ServiciosEspecificos = updatedCaso.ServiciosEspecificos;
            caso.FechaCierre = updatedCaso.FechaCierre;
            caso.ReferenciaCaso = updatedCaso.ReferenciaCaso;
            caso.MotivoDerivacion = updatedCaso.MotivoDerivacion;
            caso.NumeroSesiones = updatedCaso.NumeroSesiones;
            caso.FechaCompletitud = updatedCaso.FechaCompletitud;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaso(int id)
        {
            var caso = await _unitOfWork.Casos.GetByIdAsync(id);
            if (caso == null)
                return NotFound();

            _unitOfWork.Casos.Remove(caso);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
