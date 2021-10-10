using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Models;
using OnlineLibrary.Models.ViewModels;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Controllers
{
    public class BooksController : Controller   
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPageCountServices _pageCountServices;

        public BooksController(IBookRepository bookRepository, IPageCountServices pageCountServices)
        {
            _bookRepository = bookRepository;
            _pageCountServices = pageCountServices;
        }

        public async Task<IActionResult> Index(int? page) 
            => View(new BookViewModel("Todos os livros", 
                await _pageCountServices.GetTotalPagesCountAsync(), page,
                await _bookRepository.GetAllAsync(page)));

        public async Task<IActionResult> FindByParams(string searchString, int? page)
        {
            try
            {
                IEnumerable<Book> books = await _bookRepository.FindByStringParamsAsync(searchString, page);
                int totalPages = await _pageCountServices.GetTotalPagesCountWithSearchParametersAsync(searchString);
                return View(new BookViewModel("Pesquisa por título", 
                    $"Resultados para: {searchString}", totalPages, page, books));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public async Task<IActionResult> GetByGenre(int genreId, int? page)
        {
            try
            {
                IEnumerable<Book> books = await _bookRepository.GetByGenre(genreId, page);
                int totalPages = await _pageCountServices.GetTotalPagesCountSearchingByGenre(genreId);
                string genreName = books.First().Genre.Name;
                return View(new BookViewModel("Livros por gênero", $"Resultados para: {genreName}",
                    totalPages, page, books, genreId));
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