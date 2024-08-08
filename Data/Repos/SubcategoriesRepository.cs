using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class SubcategoriesRepository : ISubcategoriesRepository
    {
        private readonly ApplicationDbContext _context;

        public SubcategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<Subcategory>>> GetAll()
        {
            var taskResult = new TaskResult<List<Subcategory>>() { Success = true, Model = new List<Subcategory>(), Message = "" };

            try
            {
                var subcategories = await _context.Subcategories.ToListAsync();

                if (subcategories == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subcategories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subcategories;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }



        public async Task<TaskResult<List<Subcategory>>> GetAllByCategoryId(string categoryId)
        {
            var taskResult = new TaskResult<List<Subcategory>>() { Success = true, Model = new List<Subcategory>() { }, Message = "" };

            try
            {
                var subcategories = await _context.Subcategories.Where(w=> w.CategoryId == categoryId).ToListAsync ();
                if (subcategories == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subcategories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subcategories;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }



        public async Task<TaskResult<Subcategory>> Get(string subcategoryId)
        {
            var taskResult = new TaskResult<Subcategory>() { Success = true, Model = new Subcategory(), Message = "" };

            try
            {
                var subcategory = await _context.Subcategories.FirstOrDefaultAsync(f => f.SubcategoryId == subcategoryId);
                if (subcategory == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subategory was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = subcategory;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }

         




        public async Task<TaskResult<Subcategory>> Create(Subcategory model)
        {
            var taskResult = new TaskResult<Subcategory>() { Success = true, Model = new Subcategory(), Message = "" };

            if (model != null)
            {
                try
                {
                    var subcategory = await _context.Subcategories.FirstOrDefaultAsync(f => f.Name == model.Name);
                    if (subcategory == null)
                    {
                        model.SubcategoryId = Guid.NewGuid().ToString();
                        _context.Subcategories.Add(model);
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





        public async Task<TaskResult<Subcategory>> Update(Subcategory model)
        {
            var taskResult = new TaskResult<Subcategory>() { Success = true, Model = new Subcategory(), Message = "" };

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





        public async Task<TaskResult<Subcategory>> Delete(string subcategoryId)
        {
            var taskResult = new TaskResult<Subcategory>() { Success = true, Model = new Subcategory(), Message = "" };

            try
            {
                var subcategory = await _context.Subcategories.FirstOrDefaultAsync(f => f.SubcategoryId == subcategoryId);
                if (subcategory != null)
                {
                    _context.Subcategories.Remove(subcategory);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = subcategory;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Subcategory was null";
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
