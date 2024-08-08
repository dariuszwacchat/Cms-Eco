using Domain.Models;

namespace Data.Repos.Abs
{
    public interface IModelRepository<T>
    {
        Task<TaskResult<List<T>>> GetAll();
        Task<TaskResult<T>> Get(string id);
        Task<TaskResult<T>> Create(T model);
        Task<TaskResult<T>> Delete(string id);
        Task<TaskResult<T>> Update(T model);
    }
}
