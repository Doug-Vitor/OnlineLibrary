using Microsoft.AspNetCore.Identity;
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
        private readonly IIdentityServices _identityServices;
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IAccountRepository _accountRepository;
        private readonly IAppUserRepository _appUserRepository;

        public AccountServices(IIdentityServices identityServices, HttpContextExtensions contextExtensions,
            IAccountRepository accountRepository,  IAppUserRepository appUserRepository)
        {
            _identityServices = identityServices;
            _contextExtensions = contextExtensions;
            _accountRepository = accountRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<dynamic> SignUpAsync(UserInputViewModel inputModel)
        {
            IdentityUser user = new(inputModel.UserName);
            dynamic result = await _identityServices.CreateUserAsync(user, inputModel.Password);

            if (result is true)
                await _appUserRepository.InsertAsync(new ApplicationUser(user));

            return result;
        }

        public async Task<bool> SignInAsync(UserInputViewModel inputModel)
        {
            return await _identityServices.SignInAsync(inputModel.UserName, inputModel.Password);
        }

        public async Task SignOutAsync()
        {
            await _identityServices.SignOutAsync();
        }

        public async Task ChangeUserToAuthor(Author author)
        {
            ApplicationUser user = await _accountRepository.GetAuthenticatedUserAsync();
            author.ChangeToAuthor(user);

            _accountRepository.DetachEntity(user);
            await _accountRepository.UpdateToAuthorAsync(author);

            string userId = _contextExtensions.GetAuthenticatedUserId();
            IdentityUser identityUser = await _identityServices.FindByIdAsync(userId);
            await _identityServices.ChangeUserRoleAsync(identityUser, "Default", "Author");
        }

        public string GetErrorMessages(IdentityError error)
        {
            return IdentityErrorExtensions.TranslatedErrorDescription(error);
        }
    }
}
