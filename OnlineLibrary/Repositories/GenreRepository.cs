using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class GenreRepository : AbstractRepository, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllAsync() 
            => await _context.Genres.OrderBy(genre => genre.Name).ToListAsync();
    }
}
