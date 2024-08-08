using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsubcategoriesController : ControllerBase
    {
        private readonly ISubsubcategoriesRepository _subsubcategoriesRepository;

        public SubsubcategoriesController(ISubsubcategoriesRepository subsubcategoriesRepository)
        {
            _subsubcategoriesRepository = subsubcategoriesRepository;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<TaskResult<List<Subsubcategory>>>> GetSubsubategories()
        {
            try
            {
                var taskResult = await _subsubcategoriesRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpGet("{subsubcategoryId}")]
        public async Task<ActionResult<TaskResult<Subsubcategory>>> GetSubsubategory(string subsubcategoryId)
        {
            try
            {
                var taskResult = await _subsubcategoriesRepository.Get(subsubcategoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpGet("getAllByCategoryIdAndSubcategoryId/{categoryId}/{subcategoryId}")]
        public async Task<ActionResult<TaskResult<Subsubcategory>>> GetAllByCategoryAndSubcategoryId (string categoryId, string subcategoryId)
        {
            try
            {
                var taskResult = await _subsubcategoriesRepository.GetAllByCategoryIdAndSubCategoryId (categoryId, subcategoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<TaskResult<Subsubcategory>>> PostSubsubategory(Subsubcategory model)
        {
            try
            {
                var taskResult = await _subsubcategoriesRepository.Create(model);
                return CreatedAtAction(nameof(GetSubsubategory), new { subsubcategoryId = model.SubsubcategoryId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpPut("{subsubcategoryId}")]
        public async Task<ActionResult<TaskResult<Subsubcategory>>> PutSubsubategory(string subsubcategoryId, Subsubcategory model)
        {
            try
            {
                if (subsubcategoryId != model.SubsubcategoryId)
                    return BadRequest("SubsubcategoryId mismatch");

                var taskResult = await _subsubcategoriesRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [Authorize(Roles = "Administrator")]
        [HttpDelete("{subsubcategoryId}")]
        public async Task<ActionResult<TaskResult<Subsubcategory>>> DeleteSubsubategory(string subsubcategoryId)
        {
            try
            {
                var taskResult = await _subsubcategoriesRepository.Delete(subsubcategoryId);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
