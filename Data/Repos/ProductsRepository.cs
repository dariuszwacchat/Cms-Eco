using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class ProductsRepository : IModelRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<Product>>> GetAll()
        {
            var taskResult = new TaskResult<List<Product>>() { Success = true, Model = new List<Product>(), Message = "" };

            try
            {
                var products = await _context.Products.ToListAsync();

                if (products == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Products was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = products;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }





        public async Task<TaskResult<Product>> Get(string productId)
        {
            var taskResult = new TaskResult<Product>() { Success = true, Model = new Product(), Message = "" };

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(f => f.ProductId == productId);
                if (product == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Product was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = product;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<Product>> Create(Product model)
        {
            var taskResult = new TaskResult<Product>() { Success = true, Model = new Product(), Message = "" };

            if (model != null)
            {
                try
                {
                    model.ProductId = Guid.NewGuid().ToString();
                    _context.Products.Add(model);
                    await _context.SaveChangesAsync();

                    taskResult.Success = true;
                    taskResult.Model = model;

                }
                catch (Exception ex)
                {
                    taskResult.Success = false;
                    taskResult.Message = ex.Message;
                }
            }
            else
            {
                taskResult.Success = false;
                taskResult.Message = "Model was null";
            }
            return taskResult;
        }





        public async Task<TaskResult<Product>> Update(Product model)
        {
            var taskResult = new TaskResult<Product>() { Success = true, Model = new Product(), Message = "" };

            if (model != null)
            {
                try
                {
                    _context.Entry(model).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = model;
                }
                catch (Exception ex)
                {
                    taskResult.Success = false;
                    taskResult.Message = ex.Message;
                }
            }
            else
            {
                taskResult.Success = false;
                taskResult.Message = "Model was null";
            }
            return taskResult;
        }





        public async Task<TaskResult<Product>> Delete(string productId)
        {
            var taskResult = new TaskResult<Product>() { Success = true, Model = new Product(), Message = "" };

            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(f => f.ProductId == productId);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = product;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Product was null";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }
    }
}
