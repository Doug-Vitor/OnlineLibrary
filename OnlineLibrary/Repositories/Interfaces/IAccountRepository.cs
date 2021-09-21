using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task InsertAsync(ApplicationUser user);
        public Task ChangeUserToAuthor(ApplicationUser user);
    }
}
