using Microsoft.AspNetCore.Identity;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<dynamic> SignUpAsync(UserInputViewModel inputModel);
        Task<bool> SignInAsync(UserInputViewModel inputModel);
        Task SignOutAsync();
        Task<Author> GetAuthorAuthenticatedAsync(string userId);
        string GetErrorMessages(IdentityError error);
    }
}
