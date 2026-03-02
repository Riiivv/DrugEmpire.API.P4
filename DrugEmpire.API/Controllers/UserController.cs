using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugEmpire.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTOResponse>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTOResponse>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<UserDTOResponse>> CreateUser(UserDTORequest request)
        {
            var createdUser = await _userService.CreateUser(request);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        // PUT: api/User/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTOResponse>> UpdateUser(int id, UserDTORequest request)
        {
            var updatedUser = await _userService.UpdateUser(id, request);
            return Ok(updatedUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}