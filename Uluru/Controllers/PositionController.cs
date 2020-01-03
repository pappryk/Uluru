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

        [HttpGet("{groupId}")]
        public async Task<ActionResult> GetAllOfGroupAsync(int groupId)
        {
            var workEntries = await _positionRepository.GetAllOfGroupAsync(groupId);
            return new JsonResult(workEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workEntry = await _positionRepository.GetById(id);
            return new JsonResult(workEntry);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>)"]
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
                return Problem(e.Message);
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
