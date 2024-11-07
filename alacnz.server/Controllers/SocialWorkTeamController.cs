using alacnz.server.Data.Repositories.Interfaces;
using alacnz.server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alacnz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialWorkTeamController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SocialWorkTeamController> _logger;

        public SocialWorkTeamController(IUnitOfWork unitOfWork, ILogger<SocialWorkTeamController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/socialworkteams
        [HttpGet]
        public async Task<IActionResult> GetAllSocialWorkTeams()
        {
            try
            {
                var teams = await _unitOfWork.SocialWorkTeams.GetAllAsync();
                if (teams == null || !teams.Any())
                {
                    return NotFound("No social work teams found.");
                }
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving social work teams.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/socialworkteams/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialWorkTeamById(int id)
        {
            try
            {
                var team = await _unitOfWork.SocialWorkTeams.GetByIdAsync(id);
                if (team == null)
                    return NotFound($"Social Work Team with ID {id} not found.");

                return Ok(team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the social work team with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/socialworkteams
        [HttpPost]
        public async Task<IActionResult> CreateSocialWorkTeam([FromBody] SocialWorkTeam team)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new social work team.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.SocialWorkTeams.AddAsync(team);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetSocialWorkTeamById), new { id = team.SocialWorkTeamId }, team);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the social work team.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/socialworkteams/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSocialWorkTeam(int id, [FromBody] SocialWorkTeam updatedTeam)
        {
            if (id != updatedTeam.SocialWorkTeamId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for social work team ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingTeam = await _unitOfWork.SocialWorkTeams.GetByIdAsync(id);
                if (existingTeam == null)
                    return NotFound($"Social Work Team with ID {id} not found.");

                // Update properties of the existing social work team
                existingTeam.Name = updatedTeam.Name;
                existingTeam.City = updatedTeam.City;
                existingTeam.Specialty = updatedTeam.Specialty;

                // If you need to update cases or other relationships, do that here
                // For example, we might update the cases assigned to this team
                existingTeam.Cases = updatedTeam.Cases;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the social work team with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/socialworkteams/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialWorkTeam(int id)
        {
            try
            {
                var team = await _unitOfWork.SocialWorkTeams.GetByIdAsync(id);
                if (team == null)
                {
                    return NotFound($"Social Work Team with ID {id} not found.");
                }

                await _unitOfWork.SocialWorkTeams.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the social work team with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
