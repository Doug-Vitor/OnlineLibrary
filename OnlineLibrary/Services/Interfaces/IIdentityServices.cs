using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IIdentityServices
    {
        Task<IdentityUser> FindByIdAsync(string userId);
        Task<dynamic> CreateUserAsync(IdentityUser user, string password);
        Task ChangeUserRoleAsync(IdentityUser user, string oldRole, string newRole);
        Task<bool> SignInAsync(string userName, string password);
        Task SignOutAsync();
    }
}
