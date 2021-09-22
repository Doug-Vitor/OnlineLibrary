using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task InsertAsync(ApplicationUser user);
        Task<Author> GetByIdAsync(string userId);
        Task ChangeUserToAuthor(ApplicationUser user);
    }
}
