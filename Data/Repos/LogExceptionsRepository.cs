using Data.Repos.Abs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repos
{
    public class LogExceptionsRepository : IModelRepository<LogException>
    {
        private readonly ApplicationDbContext _context;

        public LogExceptionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<TaskResult<List<LogException>>> GetAll()
        {
            var taskResult = new TaskResult<List<LogException>>() { Success = true, Model = new List<LogException>(), Message = "" };

            try
            {
                var logExceptions = await _context.LogExceptions.ToListAsync();

                if (logExceptions == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "LogExceptions was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = logExceptions;
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





        public async Task<TaskResult<LogException>> Get(string logExceptionId)
        {
            var taskResult = new TaskResult<LogException>() { Success = true, Model = new LogException(), Message = "" };

            try
            {
                var logException = await _context.LogExceptions.FirstOrDefaultAsync(f => f.LogExceptionId == logExceptionId);
                if (logException == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Category was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = logException;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }




        public async Task<TaskResult<LogException>> Create(LogException model)
        {
            var taskResult = new TaskResult<LogException>() { Success = true, Model = new LogException(), Message = "" };

            if (model != null)
            {
                try
                {
                    _context.LogExceptions.Add(model);
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





        public async Task<TaskResult<LogException>> Update(LogException model)
        {
            var taskResult = new TaskResult<LogException>() { Success = true, Model = new LogException(), Message = "" };

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





        public async Task<TaskResult<LogException>> Delete(string logExceptionId)
        {
            var taskResult = new TaskResult<LogException>() { Success = true, Model = new LogException(), Message = "" };

            try
            {
                var logException = await _context.LogExceptions.FirstOrDefaultAsync(f => f.LogExceptionId == logExceptionId);
                if (logException != null)
                {
                    _context.LogExceptions.Remove(logException);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                    taskResult.Model = logException;
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
