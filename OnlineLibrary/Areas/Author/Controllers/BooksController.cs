using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Areas.Author.Models;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineLibrary.Areas.Author.Controllers
{
    [Area("Author")]
    [Authorize(Roles = "Author")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAccountServices _accountServices;
        private readonly IImageManagerServices _imageManagerServices;

        public BooksController(IBookRepository bookRepository, IAccountServices accountServices,
            IImageManagerServices imageManagerServices)
        {
            _bookRepository = bookRepository;
            _accountServices = accountServices;
            _imageManagerServices = imageManagerServices;
        }

        public async Task <IActionResult> Create()
        {
            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new BookInputViewModel(
                await _accountServices.GetAuthorAuthenticatedAsync(authenticatedUserId)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _bookRepository.InsertAsync(book);
                return RedirectToAction(nameof(Index), "Home");
            }

            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(new BookInputViewModel(
                await _accountServices.GetAuthorAuthenticatedAsync(authenticatedUserId), book));
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
        public async Task<IActionResult> Edit(Book book, IFormFile file)
        {
            string imageUpload = await _imageManagerServices.UploadBookImageAsync(file, book.Id);
            if (string.IsNullOrWhiteSpace(imageUpload))
            {
                if (ModelState.IsValid)
                {
                    book.ImagePath = $"~/Images/BookImages/{book.Id}.png";
                    await _bookRepository.UpdateAsync(book);
                    return RedirectToAction(nameof(Index), "Home");
                }
            }
            else
                ModelState.AddModelError(string.Empty, imageUpload);

            string authenticatedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            book.Author = await _accountServices.GetAuthorAuthenticatedAsync(authenticatedUserId);
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
