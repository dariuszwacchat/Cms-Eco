using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IModelRepository<Category> _categoriesRepository;

        public CategoriesController(IModelRepository<Category> categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TaskResult<List<Category>>>> GetCategories()
        {
            try
            {
                var taskResult = await _categoriesRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<Category>>> GetCategory(string id)
        {
            try
            {
                var taskResult = await _categoriesRepository.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<TaskResult<Category>>> PostCategory(Category model)
        {
            try
            {
                var taskResult = await _categoriesRepository.Create(model);
                return CreatedAtAction(nameof(GetCategory), new { id = model.CategoryId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<Category>>> PutCategory(string id, Category model)
        {
            try
            {
                if (id != model.CategoryId)
                    return BadRequest("CategoryId mismatch");

                var taskResult = await _categoriesRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<Category>>> DeleteCategory(string id)
        {
            try
            {
                var taskResult = await _categoriesRepository.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}