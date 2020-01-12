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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Uluru.Data.Users;
using Uluru.Data.Users.DTOs;
using Uluru.DataBaseContext;
using Uluru.Helpers;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IUserRepository _usersRepository;
        private readonly AppSettings _appSettings;

        public UsersController(
            AppDbContext context,
            IUserRepository usersRepository,
            IOptions<AppSettings> appSettings)
        {
            //context.Database.EnsureCreated();
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetUsers()
        {
            var users = await _usersRepository.GetAllAsync();
            var result = users.Select(u => new UserDetailDTO(u));

            return new ActionResult<IEnumerable<UserDetailDTO>>(result);
        }

        [HttpGet("group/{id}")]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetUsersOfGroup([FromRoute]int id)
        {
            var users = await _usersRepository.GetAllUsersOfGroupAsync(id);
            var result = users.Select(u => new UserDetailDTO(u));

            return new ActionResult<IEnumerable<UserDetailDTO>>(result);
        }

        [HttpGet("group/fromClaims")]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetUsersOfGroupFromClaims()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "GroupId");
            if (idClaim == null)
                return Unauthorized();

            int groupId = Int32.Parse(idClaim.Value.ToString());

            var users = await _usersRepository.GetAllUsersOfGroupAsync(groupId);
            var result = users.Select(u => new UserDetailDTO(u));

            return new ActionResult<IEnumerable<UserDetailDTO>>(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            var user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            
            var result = new UserDetailDTO(user);

            return result;
        }

        [HttpGet("fromClaims")]
        public async Task<ActionResult<UserDetailDTO>> GetUserFromClaims()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim == null)
                return Unauthorized();

            int id = Int32.Parse(idClaim.Value.ToString());

            var user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDetailDTO(user);

            return result;
        }

        [HttpPut("changePassword/{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] ChangePasswordDTO dto)
        {
            try
            {
                var user = await _usersRepository.UpdatePassword(id, dto.OldPassword, dto.NewPassword);
                if (user == null)
                    return Unauthorized("Bad password");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_usersRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Problem();
                }
            }
            catch (DbUpdateException)
            {
                return Problem();
            }

            //if (id != user.Id)
            //{
            //    return BadRequest();
            //}
            //_context.Entry(user).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!_usersRepository.UserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRegistrationDTO dto)
        {
            dto.UserRole = UserRole.User;
            User user = new User(dto);

            if (_usersRepository.UserWithEmailExists(user.Email))
                return Conflict();

            try
            {
                await _usersRepository.Add(user);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            return CreatedAtAction("PostUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetailDTO>> RemoveUser(int id)
        {
            var user = await _usersRepository.Remove(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDetailDTO(user);

            return result;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticationDTO userDto)
        {
            bool isAuthenticated = await _usersRepository.Authenticate(userDto);
            if (!isAuthenticated)
                return BadRequest("Wrong credentials");

            var user = await _usersRepository.GetByEmail(userDto.Email);

            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("WorkingGroupId", user.WorkingGroupId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties()
            {
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProps);
            var cookieOptions = new CookieOptions() 
            {
                Expires = DateTimeOffset.Now.AddDays(1)
            };

            Response.Cookies.Append("Email", user.Email, cookieOptions);
            Response.Cookies.Append("Id", user.Id.ToString(), cookieOptions);

            return Ok();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("Email");
            Response.Cookies.Delete("Id");
            return Ok();
        }
    }
}
