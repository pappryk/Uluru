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
            //_context = context;
            //context.Database.EnsureCreated();
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserGeneralDTO>>> GetUsers()
        {
            var users = await _usersRepository.GetAllAsync();
            var result = users.Select(u => new UserGeneralDTO(u));

            return new ActionResult<IEnumerable<UserGeneralDTO>>(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGeneralDTO>> GetUser(int id)
        {
            var user = await _usersRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            
            var result = new UserGeneralDTO(user);

            return result;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
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
            User user = new User(dto);

            if (_usersRepository.UserWithEmailExists(user.Email))
                return Conflict();
            else
                await _usersRepository.Add(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserGeneralDTO>> RemoveUser(int id)
        {
            var user = await _usersRepository.Remove(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = new UserGeneralDTO(user);

            return result;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticationDTO userDto)
        {
            bool isAuthenticated = await _usersRepository.Authenticate(userDto);
            if (!isAuthenticated)
                return BadRequest("Wrong credentials");

            var user = _usersRepository.GetByEmail(userDto.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("", "");
        }
    }
}
