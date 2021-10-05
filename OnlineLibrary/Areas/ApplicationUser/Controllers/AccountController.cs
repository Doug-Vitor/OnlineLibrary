using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.ApplicationUser.Controllers
{
    [Area("ApplicationUser")]
    [Authorize(Roles="Author")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IImageManagerServices _imageManagerServices;

        public AccountController(IAccountRepository accountRepository, IAuthorRepository authorRepository, 
            IBookRepository bookRepository, IImageManagerServices imageManagerServices)
        {
            _accountRepository = accountRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _imageManagerServices = imageManagerServices;
        }

        public async Task<IActionResult> MyProfile()
        {
            return View(await _accountRepository.GetAuthenticatedUserAsync());
        }
        
        public async Task<IActionResult> MyBooks()
        {
            return View(await _bookRepository.GetByAuthorAuthenticatedAsync());
        }

        public async Task<IActionResult> Edit()
        {
            return View(await _accountRepository.GetAuthenticatedUserAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author, IFormFile imageFile)
        {
            string imageUploadResult = await _imageManagerServices.UploadProfileImageAsync(imageFile, author.Id);

            if (string.IsNullOrWhiteSpace(imageUploadResult))
            {
                if (ModelState.IsValid)
                {
                    author.ImagePath = $"~/Images/ProfilePhotos/{author.Id}.png";
                    await _authorRepository.UpdateAsync(author);
                    return RedirectToAction(nameof(MyProfile));
                }
            }

            ModelState.AddModelError(string.Empty, imageUploadResult);
            return View(author);
        }
    }
}
