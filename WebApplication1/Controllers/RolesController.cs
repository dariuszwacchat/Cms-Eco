using Application.Services.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TaskResult<List<ApplicationRole>>>> GetRoles()
        {
            try
            {
                var taskResult = await _rolesService.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<ApplicationRole>>> GetRole(string id)
        {
            try
            {
                var taskResult = await _rolesService.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<TaskResult<ApplicationRole>>> PostRole(ApplicationRole model)
        {
            try
            {
                var taskResult = await _rolesService.Create(model);
                return CreatedAtAction(nameof(GetRole), new { id = model.Id }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<ApplicationRole>>> PutRole(string id, ApplicationRole model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest("RoleId mismatch");

                var taskResult = await _rolesService.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<ApplicationRole>>> DeleteRole(string id)
        {
            try
            {
                var taskResult = await _rolesService.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
