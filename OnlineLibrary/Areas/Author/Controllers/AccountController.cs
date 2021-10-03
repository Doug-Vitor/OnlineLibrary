using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Extensions;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles="Author")]
    public class AccountController : Controller
    {
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IAccountServices _accountServices;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageManagerServices _imageManagerServices;

        public AccountController(HttpContextExtensions contextExtensions, 
            IAccountServices accountServices, IAccountRepository accountRepository, 
            IAuthorRepository authorRepository, IImageManagerServices imageManagerServices)
        {
            _contextExtensions = contextExtensions;
            _accountServices = accountServices;
            _accountRepository = accountRepository;
            _authorRepository = authorRepository;
            _imageManagerServices = imageManagerServices;
        }

        public async Task<IActionResult> MyProfile()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return View(await _accountRepository.GetAuthenticatedAuthorByIdAsync(authenticatedUserId));
        }

        public async Task<IActionResult> Edit()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return View(await _accountRepository.GetAuthenticatedAuthorByIdAsync(authenticatedUserId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OnlineLibrary.Models.Author author, IFormFile imageFile)
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
