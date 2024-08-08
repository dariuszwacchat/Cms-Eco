using Domain.Models;
using Domain.ViewModels;

namespace Application.Services.Abs
{
    public interface IUsersService
    {
        Task<TaskResult<List<ApplicationUser>>> GetAll();
        Task<TaskResult<ApplicationUser>> GetUserById(string id);
        Task<TaskResult<ApplicationUser>> GetUserByEmail(string email);
        Task<TaskResult<RegisterViewModel>> Create(RegisterViewModel model);
        Task<TaskResult<ApplicationUser>> Update(ApplicationUser model);
        Task<TaskResult<ApplicationUser>> Delete(string id);
    }
}
