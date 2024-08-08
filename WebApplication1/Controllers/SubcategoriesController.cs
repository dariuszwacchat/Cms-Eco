using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoriesRepository _subcategoriesRepository;

        public SubcategoriesController(ISubcategoriesRepository subcategoriesRepository)
        {
            _subcategoriesRepository = subcategoriesRepository;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TaskResult<List<Subcategory>>>> GetSubcategories()
        {
            try
            {
                var taskResult = await _subcategoriesRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [AllowAnonymous]
        [HttpGet("{subcategoryId}")]
        public async Task<ActionResult<TaskResult<Subcategory>>> GetSubcategory(string subcategoryId)
        {
            try
            {
                var taskResult = await _subcategoriesRepository.Get(subcategoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpGet("GetAllByCategoryId/{categoryId}")]
        public async Task<ActionResult<TaskResult<List<Subcategory>>>> GetAllByCategoryId(string categoryId)
        {
            try
            {
                var taskResult = await _subcategoriesRepository.GetAllByCategoryId(categoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<TaskResult<Subcategory>>> PostSubcategory(Subcategory model)
        {
            try
            {
                var taskResult = await _subcategoriesRepository.Create(model);
                return CreatedAtAction(nameof(GetSubcategory), new { subcategoryId = model.SubcategoryId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpPut("{subcategoryId}")]
        public async Task<ActionResult<TaskResult<Subcategory>>> PutSubcategory(string subcategoryId, Subcategory model)
        {
            try
            {
                if (subcategoryId != model.SubcategoryId)
                    return BadRequest("SubcategoryId mismatch");

                var taskResult = await _subcategoriesRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpDelete("{subcategoryId}")]
        public async Task<ActionResult<TaskResult<Subcategory>>> DeleteSubcategory(string subcategoryId)
        {
            try
            {
                var taskResult = await _subcategoriesRepository.Delete(subcategoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
