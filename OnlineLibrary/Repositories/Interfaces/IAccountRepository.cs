using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        void DetachEntity(ApplicationUser user);
        Task<ApplicationUser> GetAuthenticatedUserAsync();
        Task UpdateToAuthorAsync(Author author);
    }
}
