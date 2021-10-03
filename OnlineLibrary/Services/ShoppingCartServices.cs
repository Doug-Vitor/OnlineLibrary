using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IShoppingCartItemsRepository _cartItemsRepository;

        public ShoppingCartServices(IBookRepository bookRepository,
            IShoppingCartItemsRepository cartItemsRepository)
        {
            _bookRepository = bookRepository;
            _cartItemsRepository = cartItemsRepository;
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
                cartItem.DecreaseQuantity();
                await _cartItemsRepository.UpdateAsync(cartItem);
            }
        }

        public async Task RemoveItemFromCartAsync(int itemId)
        {
            await _cartItemsRepository.RemoveAsync(itemId);
        }
    }
}
