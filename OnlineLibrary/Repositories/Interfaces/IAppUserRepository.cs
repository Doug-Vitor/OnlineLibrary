using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        Task InsertAsync(ApplicationUser user);
        Task RemoveAsync(ApplicationUser user);
    }
}
