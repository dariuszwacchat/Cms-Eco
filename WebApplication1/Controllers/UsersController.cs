using Application.Services.Abs;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<TaskResult<List<ApplicationUser>>>> GetUsers()
        {
            try
            {
                var taskResult = await _usersService.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<TaskResult<ApplicationUser>>> GetUserById(string id)
        {
            try
            {
                var taskResult = await _usersService.GetUserById(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult<TaskResult<ApplicationUser>>> GetUserByEmail(string email)
        {
            try
            {
                var taskResult = await _usersService.GetUserByEmail(email);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<TaskResult<ApplicationUser>>> PostUser(RegisterViewModel model)
        {
            try
            {
                var taskResult = await _usersService.Create(model);
                return CreatedAtAction(nameof(GetUserById), new { id = model.UserId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<ApplicationUser>>> PutUser(string id, ApplicationUser model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest("ApplicationUser mismatch");

                var taskResult = await _usersService.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<ApplicationUser>>> DeleteUser (string id)
        {
            try
            {
                var taskResult = await _usersService.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
