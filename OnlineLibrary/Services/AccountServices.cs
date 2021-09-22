using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountRepository _accountRepository;

        public AccountServices(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<dynamic> SignUpAsync(UserInputViewModel inputModel)
        {
            IdentityUser user = new(inputModel.UserName);

            var result = await _userManager.CreateAsync(user, inputModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Default");
                await _signInManager.SignInAsync(user, false);
                await _accountRepository.InsertAsync(new ApplicationUser(user));
                return true;
            }

            return result.Errors;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> SignInAsync(UserInputViewModel inputModel)
        {
            var user = await _userManager.FindByNameAsync(inputModel.UserName);
            if (user is null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, inputModel.Password, false, false);
            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Author> GetAuthorAuthenticatedAsync(string userId)
        {
            return await _accountRepository.GetByIdAsync(userId);
        }

        public string GetErrorMessages(IdentityError error)
        {
            if (IdentityErrorExtensions.ErrorIsSafeToShare(error))
                return IdentityErrorExtensions.TranslateErrorDescription(error.Code);

            return "Ocorreu um erro desconhecido.";
        }
    }
}
