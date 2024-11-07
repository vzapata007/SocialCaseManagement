using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CaseController> _logger;

        public CaseController(IUnitOfWork unitOfWork, ILogger<CaseController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/cases
        // Retrieves all cases from the database
        [HttpGet]
        public async Task<IActionResult> GetAllCases()
        {
            try
            {
                var cases = await _unitOfWork.Cases.GetAllAsync();
                if (cases == null || !cases.Any())  // Use Any() to check for elements
                {
                    return NotFound("No cases found.");
                }
                return Ok(cases);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving cases.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/cases/{id}
        // Retrieves a specific case by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            try
            {
                var caseItem = await _unitOfWork.Cases.GetByIdAsync(id);
                if (caseItem == null)
                    return NotFound($"Case with ID {id} not found.");

                return Ok(caseItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the case with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/cases
        // Creates a new case in the database
        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] Case caseItem)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new case.");
                return BadRequest(ModelState);
            }

            try
            {
                // Check if the ClientId, ServiceId, or SocialWorkTeamId are valid if necessary
                if (caseItem.ClientId == null || caseItem.ServiceId == null || caseItem.SocialWorkTeamId == null)
                {
                    _logger.LogWarning("One or more required foreign keys are missing in the case.");
                    return BadRequest("Required fields (ClientId, ServiceId, SocialWorkTeamId) cannot be null.");
                }

                await _unitOfWork.Cases.AddAsync(caseItem);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetCaseById), new { id = caseItem.CaseId }, caseItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the case.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/cases/{id}
        // Updates an existing case by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase(int id, [FromBody] Case updatedCase)
        {
            if (id != updatedCase.CaseId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for case ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingCase = await _unitOfWork.Cases.GetByIdAsync(id);
                if (existingCase == null)
                    return NotFound($"Case with ID {id} not found.");

                // Update properties of the existing case
                existingCase.RegistrationDate = updatedCase.RegistrationDate;
                existingCase.ClosureDate = updatedCase.ClosureDate;
                existingCase.Status = updatedCase.Status;
                existingCase.City = updatedCase.City;
                existingCase.Services = updatedCase.Services;
                existingCase.ReferredBy = updatedCase.ReferredBy;
                existingCase.ReferralReason = updatedCase.ReferralReason;
                existingCase.ActivationFeasibility = updatedCase.ActivationFeasibility;
                existingCase.ClientId = updatedCase.ClientId;
                existingCase.SocialWorkTeamId = updatedCase.SocialWorkTeamId;
                existingCase.ServiceId = updatedCase.ServiceId;
                existingCase.Beneficiaries = updatedCase.Beneficiaries;
                existingCase.Sessions = updatedCase.Sessions;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the case with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/cases/{id}
        // Deletes a case by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            try
            {
                var caseItem = await _unitOfWork.Cases.GetByIdAsync(id);
                if (caseItem == null)
                    return NotFound($"Case with ID {id} not found.");

                _unitOfWork.Cases.Remove(caseItem);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the case with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}