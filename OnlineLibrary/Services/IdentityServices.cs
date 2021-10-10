using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class IdentityServices : IIdentityServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityServices(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
            => await _userManager.FindByIdAsync(userId);

        public async Task ChangeUserRoleAsync(IdentityUser user, string oldRole, string newRole)
        {
            await _userManager.RemoveFromRoleAsync(user, oldRole);
            await _userManager.AddToRoleAsync(user, newRole);
        }

        public async Task<dynamic> CreateUserAsync(IdentityUser user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Default");
                await _signInManager.SignInAsync(user, false);

                return true;
            }

            return result.Errors;
        }

        public async Task<bool> SignInAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task SignOutAsync() => await _signInManager.SignOutAsync();
    }
}
