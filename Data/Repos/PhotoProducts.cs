using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class PhotoProducts : IModelRepository<PhotoProduct>
    {
        private readonly ApplicationDbContext _context;

        public PhotoProducts(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<PhotoProduct>>> GetAll()
        {
            var taskResult = new TaskResult<List<PhotoProduct>>() { Success = true, Model = new List<PhotoProduct>(), Message = "" };

            try
            {
                var photoProducts = await _context.PhotoProducts.ToListAsync();

                if (photoProducts == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "PhotoProducts was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = photoProducts;
                    taskResult.Message = "";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }





        public async Task<TaskResult<PhotoProduct>> Get(string photoProductId)
        {
            var taskResult = new TaskResult<PhotoProduct>() { Success = true, Model = new PhotoProduct(), Message = "" };

            try
            {
                var photoProduct = await _context.PhotoProducts.FirstOrDefaultAsync(f => f.PhotoProductId == photoProductId);
                if (photoProduct == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Category was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = photoProduct;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<PhotoProduct>> Create(PhotoProduct model)
        {
            var taskResult = new TaskResult<PhotoProduct>() { Success = true, Model = new PhotoProduct(), Message = "" };

            if (model != null)
            {
                try
                {
                    model.PhotoProductId = Guid.NewGuid().ToString();
                    _context.PhotoProducts.Add(model);
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





        public async Task<TaskResult<PhotoProduct>> Update(PhotoProduct model)
        {
            var taskResult = new TaskResult<PhotoProduct>() { Success = true, Model = new PhotoProduct(), Message = "" };

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





        public async Task<TaskResult<PhotoProduct>> Delete(string photoProductId)
        {
            var taskResult = new TaskResult<PhotoProduct>() { Success = true, Model = new PhotoProduct(), Message = "" };

            try
            {
                var photoProduct = await _context.PhotoProducts.FirstOrDefaultAsync(f => f.PhotoProductId == photoProductId);
                if (photoProduct != null)
                {
                    _context.PhotoProducts.Remove(photoProduct);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = photoProduct;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "PhotoProduct was null";
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
