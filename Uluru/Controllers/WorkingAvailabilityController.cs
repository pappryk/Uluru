using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.WorkingAvailabilities;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingAvailabilityController : ControllerBase
    {
        private readonly IWorkingAvailabilityRepository _workingAvailabilityRepository;
        private readonly ILogger<WorkingAvailabilityController> _logger;
        public WorkingAvailabilityController(
            ILogger<WorkingAvailabilityController> logger,
            IWorkingAvailabilityRepository workingGroupRepository)
        {
            _workingAvailabilityRepository = workingGroupRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var workingAvailabilities = await _workingAvailabilityRepository.GetAllAsync();
            return new JsonResult(workingAvailabilities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workingAvailability = await _workingAvailabilityRepository.GetById(id);
            return new JsonResult(workingAvailability);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>")]
        [HttpPost]
        public async Task<ActionResult> PostWorkingAvailability([FromBody] WorkingAvailability workingAvailability)
        {
            try
            {
                await _workingAvailabilityRepository.Add(workingAvailability);
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

            return CreatedAtAction("PostWorkingAvailability", new { workingAvailability.Id, workingAvailability });
        }
    }
}
