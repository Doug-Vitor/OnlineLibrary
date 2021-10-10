using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Areas.ApplicationUser.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.ApplicationUser.Controllers
{
    [Area("ApplicationUser")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public HomeController(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IActionResult> Index() 
            => View(new UserPurchaseViewModel(await _purchaseRepository.GetByAuthenticatedUserAsync()));
    }
}
