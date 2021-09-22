using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles = "Author")]
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _bookRepository.GetByAuthorAuthenticatedAsync(User.Identity.Name));
        }
    }
}
