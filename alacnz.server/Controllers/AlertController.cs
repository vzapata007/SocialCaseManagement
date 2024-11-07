using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AlertController> _logger;

        public AlertController(IUnitOfWork unitOfWork, ILogger<AlertController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/alerts
        // Retrieves all alerts from the database
        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            try
            {
                var alerts = await _unitOfWork.Alerts.GetAllAsync();
                if (alerts == null || !alerts.Any())
                {
                    return NotFound("No alerts found.");
                }
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving alerts.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/alerts/{id}
        // Retrieves a specific alert by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertById(int id)
        {
            try
            {
                var alert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (alert == null)
                    return NotFound($"Alert with ID {id} not found.");

                return Ok(alert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the alert with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/alerts
        // Creates a new alert in the database
        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] Alert alert)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new alert.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.Alerts.AddAsync(alert);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetAlertById), new { id = alert.Id }, alert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the alert.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/alerts/{id}
        // Updates an existing alert by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] Alert updatedAlert)
        {
            if (id != updatedAlert.Id || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for alert ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingAlert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (existingAlert == null)
                    return NotFound($"Alert with ID {id} not found.");

                // Update properties of the existing alert
                existingAlert.FechaAlerta = updatedAlert.FechaAlerta;
                existingAlert.TipoAlerta = updatedAlert.TipoAlerta;
                existingAlert.Descripcion = updatedAlert.Descripcion;
                existingAlert.CasoId = updatedAlert.CasoId;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the alert with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/alerts/{id}
        // Deletes an alert by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            try
            {
                var alert = await _unitOfWork.Alerts.GetByIdAsync(id);
                if (alert == null)
                    return NotFound($"Alert with ID {id} not found.");

                _unitOfWork.Alerts.Remove(alert);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the alert with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}