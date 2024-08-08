using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class MarkiRepository : IModelRepository<Marka>
    {
        private readonly ApplicationDbContext _context;

        public MarkiRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<Marka>>> GetAll()
        {
            var taskResult = new TaskResult<List<Marka>>() { Success = true, Model = new List<Marka>(), Message = "" };

            try
            {
                var marki = await _context.Marka.ToListAsync();

                if (marki == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Marki was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = marki;
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





        public async Task<TaskResult<Marka>> Get(string markaId)
        {
            var taskResult = new TaskResult<Marka>() { Success = true, Model = new Marka(), Message = "" };

            try
            {
                var marka = await _context.Marka.FirstOrDefaultAsync(f => f.MarkaId == markaId);
                if (marka == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Marka was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = marka;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<Marka>> Create(Marka model)
        {
            var taskResult = new TaskResult<Marka>() { Success = true, Model = new Marka(), Message = "" };

            if (model != null)
            {
                try
                {
                    var marka = await _context.Marka.FirstOrDefaultAsync(f => f.Name == model.Name);
                    if (marka == null)
                    {
                        model.MarkaId = Guid.NewGuid().ToString();
                        _context.Marka.Add(model);
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





        public async Task<TaskResult<Marka>> Update(Marka model)
        {
            var taskResult = new TaskResult<Marka>() { Success = true, Model = new Marka(), Message = "" };

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





        public async Task<TaskResult<Marka>> Delete(string markaId)
        {
            var taskResult = new TaskResult<Marka>() { Success = true, Model = new Marka(), Message = "" };

            try
            {
                var marka = await _context.Marka.FirstOrDefaultAsync(f => f.MarkaId == markaId);
                if (marka != null)
                {
                    _context.Marka.Remove(marka);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = marka;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Marka was null";
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
