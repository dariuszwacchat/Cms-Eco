using Domain.Models;

namespace Application.Services.Abs
{
    public interface IRolesService
    {
        Task<TaskResult<List<ApplicationRole>>> GetAll();
        Task<TaskResult<ApplicationRole>> Get(string id);
        Task<TaskResult<ApplicationRole>> Create(ApplicationRole model);
        Task<TaskResult<ApplicationRole>> Update(ApplicationRole model);
        Task<TaskResult<ApplicationRole>> Delete(string id);
    }
}
