using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uluru.Data.WorkingDays;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkingDayController : ControllerBase
    {
        private readonly IWorkingDayRepository _workingDayRepository;
        private readonly ILogger<WorkingDayController> _logger;
        public WorkingDayController(
            ILogger<WorkingDayController> logger,
            IWorkingDayRepository workingDayRepository)
        {
            _workingDayRepository = workingDayRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var workingGroups = await _workingDayRepository.GetAllAsync();
            return new JsonResult(workingGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workingDay = await _workingDayRepository.GetById(id);
            return new JsonResult(workingDay);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>)"]
        [HttpPost]
        public async Task<ActionResult> PostWorkingDaySchedule([FromBody] WorkingDay workingDay)
        {
            try
            {
                await _workingDayRepository.Add(workingDay);
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

            return CreatedAtAction("PostWorkingDay", new { workingDay.Id, workingDay });
        }
    }
}
