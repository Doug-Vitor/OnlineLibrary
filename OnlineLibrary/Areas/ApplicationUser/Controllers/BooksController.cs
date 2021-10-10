using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Areas.ApplicationUser.ViewModels;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.ApplicationUser.Controllers
{
    [Area("ApplicationUser")]
    [Authorize(Roles = "Author")]
    public class BooksController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IImageManagerServices _imageManagerServices;
        private readonly IGenreRepository _genreRepository;
        private readonly IBookRepository _bookRepository;

        public BooksController(IAccountRepository accountRepository, 
            IImageManagerServices imageManagerServices, IGenreRepository genreRepository, 
            IBookRepository bookRepository)
        {
            _accountRepository = accountRepository;
            _imageManagerServices = imageManagerServices;
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
        }

        public async Task <IActionResult> Create() => 
            View(new BookInputViewModel(await _accountRepository.GetAuthenticatedUserAsync() as Author,
                await _genreRepository.GetAllAsync()));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.InsertAsync(book);
                return RedirectToAction(nameof(Index), "Home");
            }

            return View(new BookInputViewModel(
                await _accountRepository.GetAuthenticatedUserAsync() as Author,
                await _genreRepository.GetAllAsync(), book));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                return View(await _bookRepository.GetByIdAsync(id));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(error), new { message = error.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, IFormFile imageFile)
        {
            string imageUploadResult = await _imageManagerServices.UploadBookImageAsync(imageFile, book.Id);
            if (string.IsNullOrWhiteSpace(imageUploadResult))
                if (ModelState.IsValid)
                {
                    book.ImagePath = $"~/Images/BookImages/{book.Id}.png";
                    await _bookRepository.UpdateAsync(book);
                    return RedirectToAction(nameof(Index), "Home");
                }
            else
                ModelState.AddModelError(string.Empty, imageUploadResult);

            book.Author = await _accountRepository.GetAuthenticatedUserAsync() as Author;
            return View(book);
        }

        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                return View(await _bookRepository.GetByIdAsync(id));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _bookRepository.RemoveAsync(id);
                return RedirectToAction(nameof(Index), "Home");
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
