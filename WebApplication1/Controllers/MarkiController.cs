using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class MarkiController : ControllerBase
    {
        private readonly IModelRepository<Marka> _markiRepository;

        public MarkiController(IModelRepository<Marka> markiRepository)
        {
            _markiRepository = markiRepository;
        }


        [HttpGet]
        public async Task<ActionResult<TaskResult<List<Marka>>>> GetMarki()
        {
            try
            {
                var taskResult = await _markiRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<Marka>>> GetMarka(string id)
        {
            try
            {
                var taskResult = await _markiRepository.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<TaskResult<Marka>>> PostMarka(Marka model)
        {
            try
            {
                var taskResult = await _markiRepository.Create(model);
                return CreatedAtAction(nameof(GetMarka), new { id = model.MarkaId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<Marka>>> PutMarka(string id, Marka model)
        {
            try
            {
                if (id != model.MarkaId)
                    return BadRequest("Marka mismatch");

                var taskResult = await _markiRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<Marka>>> DeleteMarka(string id)
        {
            try
            {
                var taskResult = await _markiRepository.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
