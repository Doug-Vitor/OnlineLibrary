using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task InsertAsync(object user);
        public Task ChangeUserToAuthor(ApplicationUser user);
    }
}
