using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoProductsController : ControllerBase
    {
        private readonly IModelRepository<PhotoProduct> _photoProductsRepository;

        public PhotoProductsController(IModelRepository<PhotoProduct> photoProductsRepository)
        {
            _photoProductsRepository = photoProductsRepository;
        }


        [HttpGet]
        public async Task<ActionResult<TaskResult<List<PhotoProduct>>>> GetPhotoProducts()
        {
            try
            {
                var taskResult = await _photoProductsRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<PhotoProduct>>> GetPhotoProduct(string id)
        {
            try
            {
                var taskResult = await _photoProductsRepository.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<TaskResult<PhotoProduct>>> PostPhotoProduct(PhotoProduct model)
        {
            try
            {
                var taskResult = await _photoProductsRepository.Create(model);
                return CreatedAtAction(nameof(GetPhotoProduct), new { id = model.PhotoProductId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<PhotoProduct>>> PutPhotoProduct(string id, PhotoProduct model)
        {
            try
            {
                if (id != model.PhotoProductId)
                    return BadRequest("PhotoProductId mismatch");

                var taskResult = await _photoProductsRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<PhotoProduct>>> DeletePhotoProduct(string id)
        {
            try
            {
                var taskResult = await _photoProductsRepository.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
