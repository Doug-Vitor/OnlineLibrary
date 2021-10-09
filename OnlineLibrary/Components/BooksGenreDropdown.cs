using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OnlineLibrary.Components
{
    public class BooksGenreDropdown : ViewComponent
    {
        private readonly IGenreRepository _genreRepository;

        public BooksGenreDropdown(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _genreRepository.GetAllAsync());
        }
    }
}
