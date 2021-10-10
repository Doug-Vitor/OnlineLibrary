using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Repositories;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class PageCountServices : AbstractRepository, IPageCountServices
    {
        public PageCountServices(AppDbContext context) : base(context)
        {
        }

        public async Task<int> GetTotalPagesCountAsync() 
            => (int)Math.Ceiling((double)await _context.Books.CountAsync() / 15);

        public async Task<int> GetTotalPagesCountWithSearchParametersAsync(string searchString)
        {
            searchString = searchString.ToLower();
            return (int)Math.Ceiling((double)await _context
                .Books
                .Where(book => book.Title.ToLower() == searchString && 
                book.Author.FullName.ToLower() == searchString)
                .CountAsync() / 15);
        }

        public async Task<int> GetTotalPagesCountSearchingByGenre(int genreId)
            => (int)Math.Ceiling((double)await _context.Books.Where(book => book.Genre.Id == genreId).CountAsync() / 15);
    }
}
