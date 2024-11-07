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
    public class BeneficiaryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BeneficiaryController> _logger;

        public BeneficiaryController(IUnitOfWork unitOfWork, ILogger<BeneficiaryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/beneficiaries
        // Retrieves all beneficiaries from the database
        [HttpGet]
        public async Task<IActionResult> GetAllBeneficiaries()
        {
            try
            {
                var beneficiaries = await _unitOfWork.Beneficiaries.GetAllAsync();
                if (beneficiaries == null || !beneficiaries.Any())
                {
                    return NotFound("No beneficiaries found.");
                }
                return Ok(beneficiaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving beneficiaries.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/beneficiaries/{id}
        // Retrieves a specific beneficiary by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBeneficiaryById(int id)
        {
            try
            {
                var beneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(id);
                if (beneficiary == null)
                    return NotFound($"Beneficiary with ID {id} not found.");

                return Ok(beneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the beneficiary with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/beneficiaries
        // Creates a new beneficiary in the database
        [HttpPost]
        public async Task<IActionResult> CreateBeneficiary([FromBody] Beneficiary beneficiary)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new beneficiary.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.Beneficiaries.AddAsync(beneficiary);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetBeneficiaryById), new { id = beneficiary.BeneficiaryId }, beneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the beneficiary.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/beneficiaries/{id}
        // Updates an existing beneficiary by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeneficiary(int id, [FromBody] Beneficiary updatedBeneficiary)
        {
            if (id != updatedBeneficiary.BeneficiaryId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for beneficiary ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingBeneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(id);
                if (existingBeneficiary == null)
                    return NotFound($"Beneficiary with ID {id} not found.");

                // Update properties of the existing beneficiary
                existingBeneficiary.Name = updatedBeneficiary.Name;
                existingBeneficiary.Age = updatedBeneficiary.Age;
                existingBeneficiary.Category = updatedBeneficiary.Category;
                existingBeneficiary.Nationality = updatedBeneficiary.Nationality;
                existingBeneficiary.CaseId = updatedBeneficiary.CaseId;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the beneficiary with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/beneficiaries/{id}
        // Deletes a beneficiary by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            try
            {
                // Retrieve the beneficiary by id
                var beneficiary = await _unitOfWork.Beneficiaries.GetByIdAsync(id);
                if (beneficiary == null)
                {
                    return NotFound($"Beneficiary with ID {id} not found.");
                }

                // Now we call DeleteAsync from the repository
                await _unitOfWork.Beneficiaries.DeleteAsync(id);

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the beneficiary with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}