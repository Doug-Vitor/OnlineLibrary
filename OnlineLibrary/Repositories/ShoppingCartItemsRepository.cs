using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class ShoppingCartItemsRepository : AbstractRepository, IShoppingCartItemsRepository
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartItemsRepository(AppDbContext context, IShoppingCartRepository cartRepository)
            : base(context)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetAllAsync()
        {
            ShoppingCart cart = await _cartRepository.GetByAuthenticatedUserAsync();
            return await _context.ShoppingCartsItems.Where(item => item.ShoppingCart.Id == cart.Id)
                .Include(item => item.Book).ThenInclude(book => book.Author).ToListAsync();
        }

        public async Task InsertAsync(ShoppingCartItem cartItem)
        {
            cartItem.ShoppingCart = await _cartRepository.GetByAuthenticatedUserAsync();
            _context.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCartItem> GetByIdAsync(int? itemId)
        {
            if (itemId is null)
                throw new IdNotProvidedException("ID não fornecido.");

            int cartId = await _cartRepository.GetAuthenticatedUserShoppingCartId();
            return await _context.ShoppingCartsItems
                .Where(item => item.Id == itemId && item.ShoppingCart.Id == cartId).FirstOrDefaultAsync();
        }

        public async Task<ShoppingCartItem> GetByBookIdAsync(int? bookId)
        {
            if (bookId is null)
                throw new IdNotProvidedException("ID não fornecido.");

            int cartId = await _cartRepository.GetAuthenticatedUserShoppingCartId();
            return await _context.ShoppingCartsItems
                .Where(item => item.Book.Id == bookId && item.ShoppingCart.Id == cartId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ShoppingCartItem cartItem)
        {
            _context.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int itemId)
        {
            ShoppingCartItem cartItem = await GetByIdAsync(itemId);
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(ShoppingCartItem[] cartItems)
        {
            if (cartItems is null)
                return;

            _context.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
