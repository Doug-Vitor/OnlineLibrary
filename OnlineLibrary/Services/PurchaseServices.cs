using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IShoppingCartItemsRepository _cartItemsRepository;
        private readonly IAccountRepository _accountRepository;

        public PurchaseServices(IPurchaseRepository purchaseRepository, IShoppingCartItemsRepository
            cartItemsRepository, IAccountRepository accountRepository)
        {
            _purchaseRepository = purchaseRepository;
            _cartItemsRepository = cartItemsRepository;
            _accountRepository = accountRepository;
        }

        public async Task ConvertCartItemsToPurchase()
        {
            IEnumerable<ShoppingCartItem> cartItems = await _cartItemsRepository.GetAllAsync();
            Purchase purchase = new();

            foreach (ShoppingCartItem cartItem in cartItems)
            {
                PurchaseDetails purchaseDetails = new(cartItem.Book, cartItem.Quantity);
                purchase.PurchaseDetails.Add(purchaseDetails);
            }

            purchase.ApplicationUser = await _accountRepository.GetAuthenticatedUserAsync();
            await _purchaseRepository.InsertAsync(purchase);
            await _cartItemsRepository.RemoveRangeAsync(cartItems.ToArray());
        }
    }
}
