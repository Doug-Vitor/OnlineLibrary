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
        Task ChangeUserToAuthor(Author author);
        string GetErrorMessages(IdentityError error);
    }
}
