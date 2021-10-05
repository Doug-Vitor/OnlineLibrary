using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartServices _cartServices;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IShoppingCartItemsRepository _cartItemsRepository;
        private readonly IPurchaseServices _purchaseServices;

        public ShoppingCartController(IShoppingCartRepository cartRepository, 
            IShoppingCartServices cartServices, IShoppingCartItemsRepository cartItemsRepository,
            IPurchaseServices purchaseServices)
        {
            _cartServices = cartServices;
            _cartRepository = cartRepository;
            _cartItemsRepository = cartItemsRepository;
            _purchaseServices = purchaseServices;
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            return View(await _cartRepository.GetByAuthenticatedUserAsync());
        }

        public async Task<RedirectToActionResult> AddBookToCart(int id)
        {
            try
            {
                await _cartServices.AddItemToCartAsync(id);
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<RedirectToActionResult> IncreaseItemQuantity(int itemId)
        {
            try
            {
                await _cartServices.IncreaseItemQuantityAsync(itemId);
            }
            catch (ApplicationException error)
            {

                return RedirectToAction(nameof(Error), new { message = error.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<RedirectToActionResult> DecreaseItemQuantity(int itemId)
        {
            await _cartServices.DecreaseItemQuantityAsync(itemId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<RedirectToActionResult> RemoveItemFromCart(int itemId)
        {
            await _cartServices.RemoveItemFromCartAsync(itemId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout()
        {
            return View(new CheckoutViewModel(await _cartItemsRepository.GetAllAsync()));
        }

        public async Task<RedirectToActionResult> ConfirmPurchase()
        {
            await _purchaseServices.ConvertCartItemsToPurchase();
            return RedirectToAction(nameof(Index), "Home", new { area = "ApplicationUser" });
        }

        public async Task<RedirectToActionResult> CancelCart()
        {
            await _cartServices.CancelCartAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        [IgnoreAntiforgeryToken]
        public IActionResult Error(string message)
        {
            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(requestId, message));
        }
    }
}
