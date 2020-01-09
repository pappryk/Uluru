using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.WorkEntries;
using Uluru.Data.WorkingDays;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    //[Authorize]
    public class WorkEntryController : ControllerBase
    {
        private readonly IWorkEntryRepository _workEntryRepository;
        private readonly ILogger<WorkingDayController> _logger;
        public WorkEntryController(
            ILogger<WorkingDayController> logger,
            IWorkEntryRepository workEntryRepository)
        {
            _workEntryRepository = workEntryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var workEntries = await _workEntryRepository.GetAllAsync();
            return new JsonResult(workEntries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workEntry = await _workEntryRepository.GetById(id);
            return new JsonResult(workEntry);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>)"]
        [HttpPost]
        public async Task<ActionResult> PostWorkingGroupSchedule([FromBody] IEnumerable<WorkEntry> workEntries)
        {
            try
            {
                await _workEntryRepository.AddMany(workEntries);
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

            return CreatedAtAction("PostWorkEntries", workEntries.Select(w => w.Id).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            WorkEntry result;
            try
            {
                result = await _workEntryRepository.Remove(id);
            }
            catch(DbUpdateException)
            {
                return Problem("Cannot remove");
            }

            return Ok(result);
        }
    }
}
