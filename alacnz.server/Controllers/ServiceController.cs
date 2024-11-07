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
    public class ServiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IUnitOfWork unitOfWork, ILogger<ServiceController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/services
        // Retrieves all services from the database
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var services = await _unitOfWork.Services.GetAllAsync();
                if (services == null || !services.Any())
                {
                    return NotFound("No services found.");
                }
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving services.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/services/{id}
        // Retrieves a specific service by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            try
            {
                var service = await _unitOfWork.Services.GetByIdAsync(id);
                if (service == null)
                    return NotFound($"Service with ID {id} not found.");

                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the service with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/services
        // Creates a new service in the database
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] Service service)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new service.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.Services.AddAsync(service);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetServiceById), new { id = service.ServiceId }, service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the service.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/services/{id}
        // Updates an existing service by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Service updatedService)
        {
            if (id != updatedService.ServiceId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for service ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingService = await _unitOfWork.Services.GetByIdAsync(id);
                if (existingService == null)
                    return NotFound($"Service with ID {id} not found.");

                // Update properties of the existing service
                existingService.ServiceType = updatedService.ServiceType;
                existingService.Description = updatedService.Description;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the service with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/services/{id}
        // Deletes a service by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var service = await _unitOfWork.Services.GetByIdAsync(id);
                if (service == null)
                    return NotFound($"Service with ID {id} not found.");

                _unitOfWork.Services.Remove(service);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the service with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}