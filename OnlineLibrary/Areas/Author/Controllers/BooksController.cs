using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Areas.Author.Models;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles = "Author")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IAccountRepository _accountRepository;
        private readonly IImageManagerServices _imageManagerServices;

        public BooksController(IBookRepository bookRepository, 
            HttpContextExtensions contextExtensions, 
            IAccountRepository accountRepository,  IImageManagerServices imageManagerServices)
        {
            _bookRepository = bookRepository;
            _contextExtensions = contextExtensions;
            _accountRepository = accountRepository;
            _imageManagerServices = imageManagerServices;
        }

        public async Task <IActionResult> Create()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return View(new BookInputViewModel(
                await _accountRepository.GetAuthenticatedAuthorByIdAsync(authenticatedUserId)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.InsertAsync(book);
                return RedirectToAction(nameof(Index), "Home");
            }

            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return View(new BookInputViewModel(
                await _accountRepository.GetAuthenticatedAuthorByIdAsync(authenticatedUserId), book));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                return View(await _bookRepository.GetAuthorByIdAsync(id));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(error), new { message = error.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book, IFormFile imageFile)
        {
            string imageUploadResult = await _imageManagerServices.UploadBookImageAsync(imageFile, book.Id);
            if (string.IsNullOrWhiteSpace(imageUploadResult))
            {
                if (ModelState.IsValid)
                {
                    book.ImagePath = $"~/Images/BookImages/{book.Id}.png";
                    await _bookRepository.UpdateAsync(book);
                    return RedirectToAction(nameof(Index), "Home");
                }
            }
            else
                ModelState.AddModelError(string.Empty, imageUploadResult);

            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            book.Author = await _accountRepository.GetAuthenticatedAuthorByIdAsync(authenticatedUserId);
            return View(book);
        }

        public async Task<IActionResult> Remove(int? id)
        {
            try
            {
                return View(await _bookRepository.GetAuthorByIdAsync(id));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(error), new { message = error.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _bookRepository.RemoveAsync(id);
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(error), new { message = error.Message });
            }
        }

        public IActionResult Error(string message)
        {
            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(requestId, message));
        }
    }
}
