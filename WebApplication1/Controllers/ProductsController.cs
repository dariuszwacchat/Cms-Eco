using Data.Repos.Abs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IModelRepository<Product> _productsRepository;

        public ProductsController(IModelRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<TaskResult<List<Product>>>> GetProducts()
        {
            try
            {
                var taskResult = await _productsRepository.GetAll();
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResult<Product>>> GetProduct(string id)
        {
            try
            {
                var taskResult = await _productsRepository.Get(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPost]
        public async Task<ActionResult<TaskResult<Product>>> PostProduct(Product model)
        {
            try
            {
                var taskResult = await _productsRepository.Create(model);
                return CreatedAtAction(nameof(GetProduct), new { id = model.ProductId }, taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut("{id}")]
        public async Task<ActionResult<TaskResult<Product>>> PutProduct(string id, Product model)
        {
            try
            {
                if (id != model.ProductId)
                    return BadRequest("ProductId mismatch");

                var taskResult = await _productsRepository.Update(model);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskResult<Product>>> DeleteProduct(string id)
        {
            try
            {
                var taskResult = await _productsRepository.Delete(id);
                return Ok(taskResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
