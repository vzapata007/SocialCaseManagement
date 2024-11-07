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
    public class ClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IUnitOfWork unitOfWork, ILogger<ClientController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/clients
        // Retrieves all clients from the database
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _unitOfWork.Clients.GetAllAsync();
                if (clients == null || !clients.Any())
                {
                    return NotFound("No clients found.");
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving clients.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/clients/{id}
        // Retrieves a specific client by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null)
                    return NotFound($"Client with ID {id} not found.");

                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the client with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/clients
        // Creates a new client in the database
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new client.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.Clients.AddAsync(client);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetClientById), new { id = client.ClientId }, client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the client.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/clients/{id}
        // Updates an existing client by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client updatedClient)
        {
            if (id != updatedClient.ClientId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for client ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingClient = await _unitOfWork.Clients.GetByIdAsync(id);
                if (existingClient == null)
                    return NotFound($"Client with ID {id} not found.");

                // Update properties of the existing client
                existingClient.Name = updatedClient.Name;
                existingClient.DateOfBirth = updatedClient.DateOfBirth;
                existingClient.Nationality = updatedClient.Nationality;
                existingClient.Gender = updatedClient.Gender;
                existingClient.ImmigrationStatus = updatedClient.ImmigrationStatus;
                existingClient.SocialSecurityBenefit = updatedClient.SocialSecurityBenefit;
                existingClient.BeneficiariesNationality = updatedClient.BeneficiariesNationality;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the client with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/clients/{id}
        // Deletes a client by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var client = await _unitOfWork.Clients.GetByIdAsync(id);
                if (client == null)
                    return NotFound($"Client with ID {id} not found.");

                _unitOfWork.Clients.Remove(client);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the client with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}