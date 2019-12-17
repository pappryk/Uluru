using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Uluru.Data.WorkingGroups;
using Uluru.Data.WorkingGroupSchedules;
using Uluru.DataBaseContext;
using Uluru.Helpers;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkingGroupScheduleController : ControllerBase
    {
        private readonly IWorkingGroupScheduleRepository _workingGroupScheduleRepository;
        private readonly ILogger<WorkingGroupController> _logger;
        public WorkingGroupScheduleController(
            ILogger<WorkingGroupController> logger,
            IWorkingGroupScheduleRepository workingGroupScheduleRepository)
        {
            _workingGroupScheduleRepository = workingGroupScheduleRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var workingGroups = await _workingGroupScheduleRepository.GetAllAsync();
            return new JsonResult(workingGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workingGroup = await _workingGroupScheduleRepository.GetById(id);
            return new JsonResult(workingGroup);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>)"]
        [HttpPost]
        public async Task<ActionResult> PostWorkingGroupSchedule([FromBody] WorkingGroupSchedule workingGroupSchedule)
        {
            try
            {
                await _workingGroupScheduleRepository.Add(workingGroupSchedule);
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

            return CreatedAtAction("PostWorkingScheduleGroup", new { workingGroupSchedule.Id, workingGroupSchedule });
        }
    }
}
