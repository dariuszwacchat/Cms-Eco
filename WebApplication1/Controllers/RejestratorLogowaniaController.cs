using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class RejestratorLogowaniaController : ControllerBase
    {
        private readonly IModelRepository<RejestratorLogowania> _rejestratorLogowania;

        public RejestratorLogowaniaController(IModelRepository<RejestratorLogowania> rejestratorLogowania)
        {
            _rejestratorLogowania = rejestratorLogowania;
        }



        [HttpGet]
        public async Task<ActionResult<TaskResult<List<RejestratorLogowania>>>> GetRejestratorLogowanias()
        {
            try
            {
                var taskResult = await _rejestratorLogowania.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<RejestratorLogowania>>> GetRejestratorLogowania(string id)
        {
            try
            {
                var taskResult = await _rejestratorLogowania.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<TaskResult<RejestratorLogowania>>> PostRejestratorLogowania(RejestratorLogowania model)
        {
            try
            {
                var taskResult = await _rejestratorLogowania.Create(model);
                return CreatedAtAction(nameof(GetRejestratorLogowania), new { id = model.RejestratorLogowaniaId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<RejestratorLogowania>>> PutRejestratorLogowania(string id, RejestratorLogowania model)
        {
            try
            {
                if (id != model.RejestratorLogowaniaId)
                    return BadRequest("RejestratorLogowaniaId mismatch");

                var taskResult = await _rejestratorLogowania.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<RejestratorLogowania>>> DeleteMovie(string id)
        {
            try
            {
                var taskResult = await _rejestratorLogowania.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
