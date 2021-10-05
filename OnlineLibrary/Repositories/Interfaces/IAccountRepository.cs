using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetAuthenticatedUserAsync();
    }
}
