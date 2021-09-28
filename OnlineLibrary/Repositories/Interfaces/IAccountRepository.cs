using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetAuthenticatedUserByIdAsync(string identityUserId);
        Task<Author> GetAuthenticatedAuthorByIdAsync(string identityUserId);
    }
}
