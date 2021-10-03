using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task InsertAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart> GetByAuthenticatedUserAsync();
    }
}
