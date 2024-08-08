using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SubsubcategoriesRepository : ISubsubcategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public SubsubcategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<Subsubcategory>>> GetAll()
        {
            var taskResult = new TaskResult<List<Subsubcategory>>() { Success = true, Model = new List<Subsubcategory>(), Message = "" };

            try
            {
                var subsubcategories = await _context.Subsubcategories.ToListAsync();

                if (subsubcategories == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subsubategories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subsubcategories;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }



        public async Task<TaskResult<List<Subsubcategory>>> GetAllByCategoryIdAndSubCategoryId (string categoryId, string subcategoryId)
        {
            var taskResult = new TaskResult<List<Subsubcategory>>() { Success = true, Model = new List<Subsubcategory>(), Message = "" };

            try
            {
                var subsubcategories = await _context.Subsubcategories.Where (w=>w.CategoryId == categoryId && w.SubcategoryId == subcategoryId).ToListAsync();

                if (subsubcategories == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subsubategories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subsubcategories;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }


        


        public async Task<TaskResult<Subsubcategory>> Get(string subsubcategoryId)
        {
            var taskResult = new TaskResult<Subsubcategory>() { Success = true, Model = new Subsubcategory(), Message = "" };

            try
            {
                var subsubcategory = await _context.Subsubcategories.FirstOrDefaultAsync(f => f.SubsubcategoryId == subsubcategoryId);
                if (subsubcategory == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subsubategory was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subsubcategory;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<Subsubcategory>> Create(Subsubcategory model)
        {
            var taskResult = new TaskResult<Subsubcategory>() { Success = true, Model = new Subsubcategory(), Message = "" };

            if (model != null)
            {
                try
                {
                    var subsubcategory = await _context.Subsubcategories.FirstOrDefaultAsync(f => f.Name == model.Name);
                    if (subsubcategory == null)
                    {
                        _context.Subsubcategories.Add(model);
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





        public async Task<TaskResult<Subsubcategory>> Update(Subsubcategory model)
        {
            var taskResult = new TaskResult<Subsubcategory>() { Success = true, Model = new Subsubcategory(), Message = "" };

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





        public async Task<TaskResult<Subsubcategory>> Delete(string subsubcategoryId)
        {
            var taskResult = new TaskResult<Subsubcategory>() { Success = true, Model = new Subsubcategory(), Message = "" };

            try
            {
                var subsubcategory = await _context.Subsubcategories.FirstOrDefaultAsync(f => f.SubsubcategoryId == subsubcategoryId);
                if (subsubcategory != null)
                {
                    _context.Subsubcategories.Remove(subsubcategory);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = subsubcategory;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subsubategory was null";
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
