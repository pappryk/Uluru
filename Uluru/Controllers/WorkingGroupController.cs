using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
using Microsoft.IdentityModel.Tokens;
using Uluru.Data.Users;
using Uluru.Data.Users.DTOs;
using Uluru.Data.WorkingGroups;
using Uluru.DataBaseContext;
using Uluru.Helpers;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WorkingGroupController : ControllerBase
    {
        private readonly IWorkingGroupRepository _workingGroupRepository;
        private readonly ILogger<WorkingGroupController> _logger;
        public WorkingGroupController(
            ILogger<WorkingGroupController> logger,
            IWorkingGroupRepository workingGroupRepository)
        {
            _workingGroupRepository = workingGroupRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var workingGroups = await _workingGroupRepository.GetAllAsync();
            return new JsonResult(workingGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var workingGroup = await _workingGroupRepository.GetById(id);
            return new JsonResult(workingGroup);
        }

        //[Authorize("SomeRoleOrPolicyForAdmins <admins>)"]
        [HttpPost]
        public async Task<ActionResult> PostWorkingGroup([FromBody] WorkingGroup workingGroup)
        {
            try
            {
                await _workingGroupRepository.Add(workingGroup);
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

            return Ok();
        }
    }
}
