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
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IAccountRepository _accountRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public AccountServices(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, HttpContextExtensions contextExtensions,
            IAccountRepository accountRepository,  IAppUserRepository appUserRepository, 
            IAuthorRepository authorRepository, IShoppingCartRepository cartRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextExtensions = contextExtensions;
            _accountRepository = accountRepository;
            _appUserRepository = appUserRepository;
            _authorRepository = authorRepository;
            _cartRepository = cartRepository;
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

                ApplicationUser appUser = new(user);
                await _appUserRepository.InsertAsync(appUser);
                await _cartRepository.InsertAsync(new ShoppingCart() { BuyerId = appUser.Id });

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
            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task ChangeUserToAuthor(Author author)
        {
            string userId = _contextExtensions.GetAuthenticatedUserId();
            ApplicationUser user = await _accountRepository.GetAuthenticatedUserByIdAsync(userId);
            author.UpdateFields(user.IdentityUser, user.ShoppingCart, user.Purchases);

            await _appUserRepository.RemoveAsync(user);
            await _authorRepository.InsertAsync(author);

            IdentityUser identityUser = await _userManager.FindByIdAsync(userId);
            await _userManager.RemoveFromRoleAsync(identityUser, "Default");
            await _userManager.AddToRoleAsync(identityUser, "Author");
        }

        public string GetErrorMessages(IdentityError error)
        {
            return IdentityErrorExtensions.TranslatedErrorDescription(error);
        }
    }
}
