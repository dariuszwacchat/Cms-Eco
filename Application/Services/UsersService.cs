using Application.Services.Abs;
using Data;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserSupportService _userSupportService;

        public UsersService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, UserSupportService userSupportService)
        {
            _context = context;
            _userManager = userManager;
            _userSupportService = userSupportService;
        }


        public async Task<TaskResult<List<ApplicationUser>>> GetAll()
        {
            var taskResult = new TaskResult<List<ApplicationUser>>() { Success = true, Model = new List<ApplicationUser>(), Message = "" };

            try
            {
                var users = await _context.Users.ToListAsync();

                if (users == null)
                {
                    taskResult.Success = false;
                    taskResult.Message = "Users was null";
                }
                else
                {
                    taskResult.Success = true;
                    taskResult.Model = users;
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }



        public async Task<TaskResult<ApplicationUser>> GetUserById(string id)
        {
            var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser(), Message = "" };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == id);
                if (user != null)
                {
                    taskResult.Success = true;
                    taskResult.Model = user;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Wskazany adres email nie istnieje";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }


        public async Task<TaskResult<ApplicationUser>> GetUserByEmail(string email)
        {
            var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser(), Message = "" };

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == email);
                if (user != null)
                {
                    taskResult.Success = true;
                    taskResult.Model = user;
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Wskazany adres email nie istnieje";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }
            return taskResult;
        }



        public async Task<TaskResult<RegisterViewModel>> Create(RegisterViewModel model)
        {
            var taskResult = new TaskResult<RegisterViewModel>() { Success = true, Model = new RegisterViewModel(), Message = "" };

            try
            {
                var findUser = await _userManager.FindByEmailAsync(model.Email);
                if (findUser == null)
                {
                    var user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = model.Email,
                        UserName = model.Email,

                        Imie = model.Imie,
                        Nazwisko = model.Nazwisko,
                        Ulica = model.Ulica,
                        NumerUlicy = model.NumerUlicy,
                        Miejscowosc = model.Miejscowosc,
                        Kraj = model.Kraj,
                        KodPocztowy = model.KodPocztowy,
                        DataUrodzenia = model.DataUrodzenia,
                        Telefon = model.Telefon,
                        DataDodania = DateTime.Now.ToString(),

                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        NormalizedEmail = model.Email.ToUpper(),
                        NormalizedUserName = model.Email.ToUpper(),
                        EmailConfirmed = false,
                    };


                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        string token = _userSupportService.GenerateJwtToken(user, model.RoleName);


                        var role = await _context.Roles.FirstOrDefaultAsync(f => f.Name == model.RoleName);
                        if (role != null)
                        {
                            user.RoleId = role.Id;
                            await _userManager.UpdateAsync(user);


                            // dodanie użytkownika do roli 
                            await _userSupportService.AddToRole(user, role.Id);
                        }



                        taskResult.Success = true;
                        taskResult.Model = new RegisterViewModel()
                        {
                            Email = user.Email,
                            Token = token,
                            RoleName = model.RoleName,
                        };
                        taskResult.Message = "Zarejestrowano";
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "Nie można zarejestrować użytkownika";
                    }
                }
                else
                {
                    taskResult.Success = false;
                    taskResult.Message = "Wskazany adres email już istnieje";
                }
            }
            catch (Exception ex)
            {
                taskResult.Success = false;
                taskResult.Message = ex.Message;
            }

            return taskResult;
        }





        public async Task<TaskResult<ApplicationUser>> Update(ApplicationUser model)
        {
            var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser(), Message = "" };

            try
            {
                if (model != null)
                {
                    ApplicationUser user = await _context.Users.FirstOrDefaultAsync(f => f.Id == model.Id);
                    if (user != null)
                    {
                        /*user.Email = model.Email;
                        user.UserName = model.Email;*/

                        user.Imie = model.Imie;
                        user.Nazwisko = model.Nazwisko;
                        user.Ulica = model.Ulica;
                        user.NumerUlicy = model.NumerUlicy;
                        user.Miejscowosc = model.Miejscowosc;
                        user.KodPocztowy = model.KodPocztowy;
                        user.Kraj = model.Kraj;
                        user.DataUrodzenia = model.DataUrodzenia;
                        user.Telefon = model.Telefon;


                        var userRoles = await _userManager.GetRolesAsync(user);
                        foreach (var role in userRoles)
                        {
                            await _userSupportService.RemoveFromRole(user, role);
                        }

                        user.RoleId = model.RoleId;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            // dodanie zdjęcia
                            //await CreateNewPhoto (model.Files, user.Id);

                            /*
                                                        // Usunięcie ze wszystkich rół
                                                        foreach (var role in await _context.Roles.ToListAsync ())
                                                            await _userManager.RemoveFromRoleAsync (user, role.Name);


                                                        // Przypisanie nowych ról
                                                        foreach (var selectedRole in model.SelectedRoles)
                                                        {
                                                            await _userManager.AddToRoleAsync (user, selectedRole);
                                                            await _userManager.AddClaimAsync (user, new Claim (ClaimTypes.Role, selectedRole));
                                                        }
                            */


                            /*var atr = await AddToRole(user, model.RoleId);
                            if (atr)
                            {
                                taskResult.Success = true;
                                taskResult.Model = user;
                                taskResult.Message = "Dane zostały zaktualizowane poprawnie";
                            }
                            else
                            {
                                taskResult.Success = true;
                                taskResult.Model = user;
                                taskResult.Message = "Dane zostały zaktualizowane poprawnie ale użytkownik nie został przypisany do roli";
                            }*/


                            // dodanie użytkownika do roli
                            await _userSupportService.AddToRole(user, model.RoleId);

                            taskResult.Success = true;
                            taskResult.Model = user;
                            taskResult.Message = "Dane zostały zaktualizowane poprawnie";

                        }
                        else
                        {
                            taskResult.Success = false;
                            taskResult.Message = "Dane nie zostały zaktualizowane";
                        }
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "User was null";
                    }
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





        public async Task<TaskResult<ApplicationUser>> Delete(string id)
        {
            var taskResult = new TaskResult<ApplicationUser>() { Success = true, Model = new ApplicationUser() { }, Message = "" };

            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(id);
                    if (user != null)
                    {

                        /*
                                                // usunięcie zdjęć
                                                var photosUser = await _context.PhotosUser.Where (w=> w.UserId == user.Id).ToListAsync ();
                                                foreach (var photoUser in photosUser)
                                                    _context.PhotosUser.Remove (photoUser);
                        */

                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            taskResult.Success = true;
                            taskResult.Model = user;
                        }
                    }
                    else
                    {
                        taskResult.Success = false;
                        taskResult.Message = "User was null";
                    }
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

    }
}
