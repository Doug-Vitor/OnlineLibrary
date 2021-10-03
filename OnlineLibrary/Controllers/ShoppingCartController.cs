using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartServices _cartServices;
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartController(IShoppingCartRepository cartRepository, IShoppingCartServices cartServices)
        {
            _cartServices = cartServices;
            _cartRepository = cartRepository;
        }

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
            try
            {
                await _cartServices.DecreaseItemQuantityAsync(itemId);
            }
            catch (ApplicationException error)
            {

                return RedirectToAction(nameof(Error), new { message = error.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<RedirectToActionResult> RemoveItemFromCart(int itemId)
        {
            await _cartServices.RemoveItemFromCartAsync(itemId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(requestId, message));
        }
    }
}
