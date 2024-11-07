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
    public class SessionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SessionController> _logger;

        public SessionController(IUnitOfWork unitOfWork, ILogger<SessionController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/sessions
        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            try
            {
                var sessions = await _unitOfWork.Sessions.GetAllAsync();
                if (sessions == null || !sessions.Any())
                {
                    return NotFound("No sessions found.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving sessions.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET: api/sessions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetByIdAsync(id);
                if (session == null)
                    return NotFound($"Session with ID {id} not found.");

                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the session with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST: api/sessions
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating a new session.");
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.Sessions.AddAsync(session);
                await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetSessionById), new { id = session.SessionId }, session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the session.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT: api/sessions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] Session updatedSession)
        {
            if (id != updatedSession.SessionId || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid update attempt: mismatched IDs or invalid model state for session ID {id}.");
                return BadRequest("Invalid input data.");
            }

            try
            {
                var existingSession = await _unitOfWork.Sessions.GetByIdAsync(id);
                if (existingSession == null)
                    return NotFound($"Session with ID {id} not found.");

                // Update properties of the existing session
                existingSession.SessionDate = updatedSession.SessionDate;
                existingSession.SessionType = updatedSession.SessionType;
                existingSession.Outcome = updatedSession.Outcome;
                existingSession.CaseId = updatedSession.CaseId;

                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the session with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE: api/sessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var session = await _unitOfWork.Sessions.GetByIdAsync(id);
                if (session == null)
                {
                    return NotFound($"Session with ID {id} not found.");
                }

                await _unitOfWork.Sessions.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the session with ID {id}.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
    