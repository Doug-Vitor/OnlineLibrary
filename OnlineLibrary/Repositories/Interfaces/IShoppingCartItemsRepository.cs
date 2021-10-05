using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IShoppingCartItemsRepository
    {
        Task InsertAsync(ShoppingCartItem cartItem);
        Task<IEnumerable<ShoppingCartItem>> GetAllAsync();
        Task<ShoppingCartItem> GetByIdAsync(int? itemId);
        Task<ShoppingCartItem> GetByBookIdAsync(int? bookId);
        Task UpdateAsync(ShoppingCartItem cartItem);
        Task RemoveAsync(int itemId);
        Task RemoveRangeAsync(ShoppingCartItem[] cartItems);
    }
}
