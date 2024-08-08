using Application.Services.Abs;
using Data;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }




        public async Task<TaskResult<List<ApplicationRole>>> GetAll()
        {
            var taskResult = new TaskResult<List<ApplicationRole>>() { Success = true, Model = new List<ApplicationRole>(), Message = "" };

            try
            {
                var roles = await _context.Roles.ToListAsync();
                if (roles == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Roles was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = roles;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }




        public async Task<TaskResult<ApplicationRole>> Get(string id)
        {
            var taskResult = new TaskResult<ApplicationRole>() { Success = true, Model = new ApplicationRole(), Message = "" };

            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == id);
                if (role == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Role was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = role;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }




        public async Task<TaskResult<ApplicationRole>> Create(ApplicationRole model)
        {
            var taskResult = new TaskResult<ApplicationRole>() { Success = true, Model = new ApplicationRole(), Message = "" };

            if (model != null)
            {
                try
                {
                    if ((await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.Name)) == null)
                    {

                        model.Id = Guid.NewGuid().ToString();
                        model.NormalizedName = model.Name;
                        model.ConcurrencyStamp = Guid.NewGuid().ToString();

                        _context.Roles.Add(model);
                        await _context.SaveChangesAsync();

                        taskResult.Success = true;
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "Wskazana nazwa roli już istnieje";
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




        public async Task<TaskResult<ApplicationRole>> Update(ApplicationRole model)
        {
            var taskResult = new TaskResult<ApplicationRole>() { Success = true, Model = new ApplicationRole(), Message = "" };

            if (model != null)
            {
                try
                {
                    // sprawdzenie czy podana nazwa roli już istnieje
                    if ((await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.Name)) == null)
                    {
                        var role = _context.Roles.FirstOrDefault(f => f.Id == model.Id);
                        if (role != null)
                        {
                            role.Name = model.Name;
                            _context.Entry(role).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            taskResult.Success = true;
                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Model was null";
                        }
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "Wskazana nazwa roli już istnieje";
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




        public async Task<TaskResult<ApplicationRole>> Delete(string id)
        {
            var taskResult = new TaskResult<ApplicationRole>() { Success = true, Model = new ApplicationRole(), Message = "" };

            try
            {
                var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == id);
                if (role != null)
                {
                    /*
                                        // przypisanie użytkowników do roli
                                        var userInRoles = (await _userManager.GetUsersInRoleAsync (model.Name)).ToList ();
                                        foreach (var userInRole in userInRoles)
                                            await _userManager.AddToRoleAsync (userInRole, "User");
                    */

                    // usunięcie roli
                    _context.Roles.Remove(role);
                    await _context.SaveChangesAsync();
                    taskResult.Success = true;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Model was null";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }



        private async Task<List<ApplicationUser>> UsersInRole(string roleName)
        {
            return (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
        }



    }
}
