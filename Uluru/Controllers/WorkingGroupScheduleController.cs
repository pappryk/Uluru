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
using Uluru.Data.WorkingAvailabilities;
using Uluru.Data.WorkingGroups;
using Uluru.Data.WorkingGroupSchedules;
using Uluru.DataBaseContext;
using Uluru.Helpers;
using Uluru.Models;
using Uluru.Scheduling;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkingGroupScheduleController : ControllerBase
    {
        private readonly IWorkingGroupScheduleRepository _workingGroupScheduleRepository;
        private readonly IWorkingAvailabilityRepository _workingAvailabilityRepository;
        private readonly ILogger<WorkingGroupController> _logger;
        private readonly IScheduleGenerator _scheduleGenerator;
        private readonly AppDbContext _context;
        public WorkingGroupScheduleController(
            ILogger<WorkingGroupController> logger,
            IWorkingGroupScheduleRepository workingGroupScheduleRepository,
            IWorkingAvailabilityRepository workingAvailabilities,
            IScheduleGenerator scheduleGenerator,
            AppDbContext context)
        {
            _workingGroupScheduleRepository = workingGroupScheduleRepository;
            _workingAvailabilityRepository = workingAvailabilities;
            _logger = logger;
            _scheduleGenerator = scheduleGenerator;
            _context = context;
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

       [HttpGet("generate/{scheduleId}")]
        public async Task<ActionResult> GetGeneratedSchedule(int scheduleId)
        {
            var schedule = await _workingGroupScheduleRepository.GetById(scheduleId);
            var workEntriesByPositions = schedule.WorkEntries.GroupBy(w => w.PositionId).ToList();

            foreach (var workEntriesByPosition in workEntriesByPositions)
            {
                var gaWorkEntries = workEntriesByPosition.Select(w => new GAWorkEntry(w)).ToList();
                var gaWorkingAvailabilities = (await _workingAvailabilityRepository.GetAllOfGroupAsync(schedule.WorkingGroupId))
                    .Where(w => w.User.PositionId == workEntriesByPosition.Key)
                    .Select(w => new GAAvailability(w)).ToList();

                var result = _scheduleGenerator.Generate(gaWorkEntries, gaWorkingAvailabilities)
                    .Select(w => new WorkEntry() { 
                        Id = w.Id,
                        WorkingAvailabilityId = w.Availability?.Id
                    })
                    .ToList();

                foreach (var workEntry in result)
                {
                    var entry = _context.WorkEntries.FirstOrDefault(w => w.Id == workEntry.Id);
                    entry.WorkingAvailabilityId = workEntry.WorkingAvailabilityId;
                    _context.Entry(entry).State = EntityState.Modified;
                }
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                return Problem();
            }
            catch (Exception)
            {
                return Problem();
            }

            return Ok();
        }
    }

}
