using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class RejestratorLogowaniaRepository : IModelRepository<RejestratorLogowania>
    {
        private readonly ApplicationDbContext _context;

        public RejestratorLogowaniaRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<RejestratorLogowania>>> GetAll()
        {
            var taskResult = new TaskResult<List<RejestratorLogowania>>() { Success = true, Model = new List<RejestratorLogowania>(), Message = "" };

            try
            {
                var rejestratorLogowania = await _context.RejestratorLogowania.ToListAsync();

                if (rejestratorLogowania == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Categories was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = rejestratorLogowania;
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





        public async Task<TaskResult<RejestratorLogowania>> Get(string rejestratorLogowaniaId)
        {
            var taskResult = new TaskResult<RejestratorLogowania>() { Success = true, Model = new RejestratorLogowania(), Message = "" };

            try
            {
                var rejestratorLogowania = await _context.RejestratorLogowania.FirstOrDefaultAsync(f => f.RejestratorLogowaniaId == rejestratorLogowaniaId);
                if (rejestratorLogowania == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Category was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = rejestratorLogowania;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<RejestratorLogowania>> Create(RejestratorLogowania model)
        {
            var taskResult = new TaskResult<RejestratorLogowania>() { Success = true, Model = new RejestratorLogowania(), Message = "" };

            if (model != null)
            {
                try
                {
                    //model.RejestratorLogowaniaId = Guid.NewGuid().ToString();
                    _context.RejestratorLogowania.Add(model);
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




        public async Task<TaskResult<RejestratorLogowania>> Update(RejestratorLogowania model)
        {
            var taskResult = new TaskResult<RejestratorLogowania>() { Success = true, Model = new RejestratorLogowania(), Message = "" };

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




        public async Task<TaskResult<RejestratorLogowania>> Delete(string rejestratorLogowaniaId)
        {
            var taskResult = new TaskResult<RejestratorLogowania>() { Success = true, Model = new RejestratorLogowania(), Message = "" };

            try
            {
                var rejestratorLogowania = await _context.RejestratorLogowania.FirstOrDefaultAsync(f => f.RejestratorLogowaniaId == rejestratorLogowaniaId);
                if (rejestratorLogowania != null)
                {
                    _context.RejestratorLogowania.Remove(rejestratorLogowania);
                    await _context.SaveChangesAsync();

                    taskResult.Success = true;
                    taskResult.Model = rejestratorLogowania;
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
