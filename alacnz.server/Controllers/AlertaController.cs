using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlertaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlertas()
        {
            var alertas = await _unitOfWork.Alertas.GetAllAsync();
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertaById(int id)
        {
            var alerta = await _unitOfWork.Alertas.GetByIdAsync(id);
            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlerta(Alerta alerta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _unitOfWork.Alertas.AddAsync(alerta);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetAlertaById), new { id = alerta.Id }, alerta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlerta(int id, Alerta updatedAlerta)
        {
            if (id != updatedAlerta.Id || !ModelState.IsValid)
                return BadRequest();

            var alerta = await _unitOfWork.Alertas.GetByIdAsync(id);
            if (alerta == null)
                return NotFound();

            alerta.CasoId = updatedAlerta.CasoId;
            alerta.FechaAlerta = updatedAlerta.FechaAlerta;
            alerta.TipoAlerta = updatedAlerta.TipoAlerta;
            alerta.Descripcion = updatedAlerta.Descripcion;

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerta(int id)
        {
            var alerta = await _unitOfWork.Alertas.GetByIdAsync(id);
            if (alerta == null)
                return NotFound();

            _unitOfWork.Alertas.Remove(alerta);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
