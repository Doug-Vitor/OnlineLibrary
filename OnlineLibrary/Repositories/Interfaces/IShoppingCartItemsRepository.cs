using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IShoppingCartItemsRepository
    {
        Task InsertAsync(ShoppingCartItem cartItem);
        Task<ShoppingCartItem> GetByIdAsync(int? itemId);
        Task<ShoppingCartItem> GetByBookIdAsync(int? bookId);
        Task UpdateAsync(ShoppingCartItem cartItem);
        Task RemoveAsync(int itemId);
    }
}
