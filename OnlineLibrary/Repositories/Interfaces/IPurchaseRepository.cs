using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task InsertAsync(Purchase purchase);
        Task<Purchase> GetByIdAsync(int? id);
        Task<IEnumerable<Purchase>> GetByAuthenticatedUserAsync();
    }
}
