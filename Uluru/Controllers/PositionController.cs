using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.Positions;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ILogger<PositionController> _logger;
        public PositionController(
            ILogger<PositionController> logger,
            IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
            _logger = logger;

        }

        [HttpGet("groups/{groupId}")]
        public async Task<ActionResult> GetAllOfGroupAsync(int groupId)
        {
            var positions = await _positionRepository.GetAllOfGroupAsync(groupId);
            return new JsonResult(positions);
        }

        [HttpGet("groups/fromClaims")]
        public async Task<ActionResult> GetAllOfGroupFromCredentialsAsync()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "WorkingGroupId");
            if (idClaim == null)
                return Unauthorized();

            int groupId = Int32.Parse(idClaim.Value.ToString());
            var positions = await _positionRepository.GetAllOfGroupAsync(groupId);
            return new JsonResult(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var position = await _positionRepository.GetById(id);
            return new JsonResult(position);
        }

        [HttpPost]
        public async Task<ActionResult> PostWorkingGroupSchedule([FromBody] Position position)
        {
            try
            {
                await _positionRepository.Add(position);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e.Message);
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem(e.Message);
            }

            return CreatedAtAction("PostPosition", new { position.Id, position });
        }
    }
}
