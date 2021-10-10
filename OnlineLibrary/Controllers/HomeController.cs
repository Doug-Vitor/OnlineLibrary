using Microsoft.AspNetCore.Mvc;

namespace OnlineLibrary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AccessDenied() => View();
    }
}
