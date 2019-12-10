﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uluru.Data.Users;
using Uluru.Data.Users.DTOs;
using Uluru.DataBaseContext;
using Uluru.Models;

namespace Uluru.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly IUsersRepository _usersRepository;

        public UsersController(AppDbContext context, IUsersRepository usersRepository)
        {
            //_context = context;
            _usersRepository = usersRepository;
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

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticationDTO userDto)
        {
            bool isAuthenticated = await _usersRepository.Authenticate(userDto);
            if (!isAuthenticated)
                return BadRequest("Wrong credentials");

            var user = _usersRepository.GetByEmail(userDto.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProps = new AuthenticationProperties()
            {

            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProps);

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("", "");
        }
    }
}
