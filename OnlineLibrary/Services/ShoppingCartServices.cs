using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartItemsRepository _cartItemsRepository;

        public ShoppingCartServices(IShoppingCartRepository cartRepository, IBookRepository bookRepository,
            IShoppingCartItemsRepository cartItemsRepository)
        {
            _cartRepository = cartRepository;
            _bookRepository = bookRepository;
            _cartItemsRepository = cartItemsRepository;
        }

        public async Task CreateCartAsync(ApplicationUser buyer)
        {
            ShoppingCart shoppingCart = new(buyer);
            await _cartRepository.InsertAsync(shoppingCart);
        }

        public async Task AddItemToCartAsync(int bookId)
        {
            ShoppingCartItem cartItem = await _cartItemsRepository.GetByBookIdAsync(bookId);
            if (cartItem is null)
            {
                Book book = await _bookRepository.GetByIdAsync(bookId);
                await _cartItemsRepository.InsertAsync(
                    new ShoppingCartItem(book, 1));
            }
            else
                await IncreaseItemQuantityAsync(cartItem.Id);
        }

        public async Task IncreaseItemQuantityAsync(int itemId)
        {
            ShoppingCartItem cartItem = await _cartItemsRepository.GetByIdAsync(itemId);
            if (cartItem != null)
            {
                cartItem.AddQuantity();
                await _cartItemsRepository.UpdateAsync(cartItem);
            }
        }

        public async Task DecreaseItemQuantityAsync(int itemId)
        {
            ShoppingCartItem cartItem = await _cartItemsRepository.GetByIdAsync(itemId);

            if (cartItem != null)
            {
                if (cartItem.Quantity == 1)
                {
                    await _cartItemsRepository.RemoveAsync(cartItem.Id);
                    return;
                }

                cartItem.DecreaseQuantity();
                await _cartItemsRepository.UpdateAsync(cartItem);
            }
        }

        public async Task RemoveItemFromCartAsync(int itemId)
            => await _cartItemsRepository.RemoveAsync(itemId);

        public async Task CancelCartAsync()
        {
            IEnumerable<ShoppingCartItem> cartItems = await _cartItemsRepository.GetAllAsync();
            await _cartItemsRepository.RemoveRangeAsync(cartItems.ToArray());
        }
    }
}
