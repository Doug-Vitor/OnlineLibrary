using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using OnlineLibrary.Models.Enums;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class BooksController : Controller   
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            return View(new BookViewModel("Todos os livros", page ?? 1, 
                await _bookRepository.GetPageCountAsync(), await _bookRepository.GetAllAsync(page)));
        }

        public async Task<IActionResult> FindByTitle(string searchString, int? page)
        {
            try
            {
                IEnumerable<Book> books = await _bookRepository.FindByTitleAsync(searchString, page);
                return View(new BookViewModel("Pesquisa por título", 
                    $"Resultados para: {searchString}", page ?? 1, 
                    await _bookRepository.GetPageCountAsync(), books));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public async Task<IActionResult> FindByAuthor(string searchString, int? page)
        {
            try
            {
                IEnumerable<Book> books = await _bookRepository.FindByAuthorAsync(searchString, page);
                return View(new BookViewModel("Pesquisa por autor",
                    $"Resultados para: {searchString}", page ?? 1,
                    await _bookRepository.GetPageCountAsync(), books));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public async Task<IActionResult> GetByGenre(int enumValue, int? page)
        {
            try
            {
                var genreName = EnumExtensions.GetDisplayName((Genre)enumValue);
                return View(new BookViewModel("Livros por gênero", $"Resultados para: {genreName}",
                    page ?? 1, await _bookRepository.GetPageCountAsync(),
                    await _bookRepository.GetByGenre(enumValue, page)));
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction(nameof(Error), new { message = "Não existe nenhum gênero correspondente ao valor fornecido." });
            }
        }

        public async Task<IActionResult> Details(int? id)
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

        public IActionResult Error(string message)
        {
            string requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View(new ErrorViewModel(requestId, message));
        }
    }
}
