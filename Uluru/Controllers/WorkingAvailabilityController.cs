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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workingAvailability = await _workingAvailabilityRepository.GetById(id);
            return new JsonResult(workingAvailability);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetByUserId([FromQuery]int userId)
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim == null)
                return Unauthorized();

            int id = Int32.Parse(idClaim.Value.ToString());
            var workingAvailability = await _workingAvailabilityRepository.GetAllOfUserAsync(id);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entry = await _workingAvailabilityRepository.Remove(id);

            if (entry == null)
                return NotFound();

            return Ok();
        }
    }
}
