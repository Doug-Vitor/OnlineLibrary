using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Models.ViewModels;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IAccountServices
    {
        public Task<dynamic> SignUpAsync(UserInputViewModel inputModel);
        public Task<bool> SignInAsync(UserInputViewModel inputModel);
        public Task SignOutAsync();
        public string GetErrorMessages(IdentityError error);
    }
}
