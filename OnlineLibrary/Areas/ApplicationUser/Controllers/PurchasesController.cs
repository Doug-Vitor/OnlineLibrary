using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Areas.ApplicationUser.ViewModels;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.ApplicationUser.Controllers
{
    [Area("ApplicationUser")]
    [Authorize]
    public class PurchasesController : Controller
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchasesController(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                return View(new UserPurchaseViewModel(await _purchaseRepository.GetByIdAsync(id)));
            }
            catch (AccessDeniedException)
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public IActionResult Error(string message)
        {
            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(requestId, message));
        }
    }
}
