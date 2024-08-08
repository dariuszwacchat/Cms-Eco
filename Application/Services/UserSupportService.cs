using Data;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class UserSupportService
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserSupportService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        /// <summary>
        /// Dodaje użytkownika do roli
        /// </summary>
        public async Task AddToRole(ApplicationUser user, string roleId)
        {
            try
            {
                if (user != null && !string.IsNullOrEmpty(roleId))
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == roleId);
                    if (role != null)
                    {
                        IdentityUserRole<string> userRole = new IdentityUserRole<string>()
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };
                        _context.UserRoles.Add(userRole);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }





        /// <summary>
        /// Usuwa użytkownika z roli
        /// </summary>
        public async Task RemoveFromRole(ApplicationUser user, string roleName)
        {
            try
            {
                if (user != null && !string.IsNullOrEmpty(roleName))
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(f => f.Name == roleName);
                    if (role != null)
                    {
                        var userRole = _context.UserRoles.FirstOrDefaultAsync(f => f.UserId == user.Id && f.RoleId == role.Id);
                        if (userRole != null)
                        {
                            IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>()
                            {
                                UserId = user.Id,
                                RoleId = role.Id
                            };
                            _context.UserRoles.Remove(identityUserRole);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public string GenerateJwtToken(ApplicationUser user, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
