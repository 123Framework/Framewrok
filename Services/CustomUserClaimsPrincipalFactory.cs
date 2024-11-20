using auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
namespace auth.Services
{

    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        /* public async Task<bool> AssignUserToRoleAsync(string userId, string roleName)
         {
            var user = await _userManager.FindByIdAsync(userId);
             if (user != null) {
                 var result  = await _userManager.AddToRoleAsync(user, roleName);
                 return result.Succeeded;
             }
             return false;
         }

         public async Task<bool> CreateRoleAsync(string roleName)
         {
            if (!await _roleManager.RoleExistsAsync(roleName))
             {
                 var result  = await _roleManager.CreateAsync(new IdentityRole(roleName));
                 return result.Succeeded;
             }
            return false;

         }

         public async Task<bool> DeleteRoleAsync(string roleId)
         {
             var role = await _roleManager.FindByIdAsync($"{roleId}");
             if ( role != null)
             {
                 var result = await _roleManager.DeleteAsync(role);
                 return result.Succeeded;
             }
             return false;
         }

         public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
         {
             return _roleManager.Roles.ToList();
         }

         public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName)
         {
             return await _userManager.GetUsersInRoleAsync(roleName);
         }

         public async Task<bool> RemoveUserFromRoleAsync(string userId, string roleName)
         {
             var user = await _userManager.FindByIdAsync(userId);
             if (user != null)
             {
                 var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                 return result.Succeeded;
             }
             return false;
         }*/
    }
}