using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class CategoriesRepository : IModelRepository<Category>
    {
        private readonly ApplicationDbContext _context;

        public CategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<Category>>> GetAll()
        {
            var taskResult = new TaskResult<List<Category>>() { Success = true, Model = new List<Category>(), Message = "" };

            try
            {
                var categories = await _context.Categories.ToListAsync();

                if (categories == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Categories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = categories;
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





        public async Task<TaskResult<Category>> Get(string categoryId)
        {
            var taskResult = new TaskResult<Category>() { Success = true, Model = new Category(), Message = "" };

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(f => f.CategoryId == categoryId);
                if (category == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Category was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = category;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<Category>> Create(Category model)
        {
            var taskResult = new TaskResult<Category>() { Success = true, Model = new Category(), Message = "" };

            if (model != null)
            {
                try
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(f => f.Name == model.Name);
                    if (category == null)
                    {
                        model.CategoryId = Guid.NewGuid().ToString();
                        _context.Categories.Add(model);
                        await _context.SaveChangesAsync();

                        taskResult.Success = true;
                        taskResult.Model = model;
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "Wskazana nazwa już istnieje";
                    }
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





        public async Task<TaskResult<Category>> Update(Category model)
        {
            var taskResult = new TaskResult<Category>() { Success = true, Model = new Category(), Message = "" };

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





        public async Task<TaskResult<Category>> Delete(string categoryId)
        {
            var taskResult = new TaskResult<Category>() { Success = true, Model = new Category(), Message = "" };

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(f => f.CategoryId == categoryId);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = category;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Category was null";
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