using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AuthorRepository : AbstractRepository, IAuthorRepository
    {
        private readonly IImageManagerServices _imageManagerServices;

        public AuthorRepository(AppDbContext context, IImageManagerServices imageManagerServices)
            : base(context)
        {
            _imageManagerServices = imageManagerServices;
        }

        public async Task InsertAsync(Author author)
        {
            author.ImagePath = "~/Images/ProfilePhotos/Default.png";
            _context.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author> GetByIdAsync(int? id)
        {
            if (id is null)
                throw new IdNotProvidedException("ID não fornecido");

            Author author = await _context.Authors.Where(author => author.Id == id).FirstOrDefaultAsync();
            if (author is null)
                throw new NotFoundException("Não foi possível encontrar um autor correspondente ao ID fornecido.");

            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            if (_imageManagerServices.EnsureProfilePhotoExists(author.Id) == false)
                author.ImagePath = "~/Images/ProfilePhotos/Default.png";

            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Author author = await GetByIdAsync(id);
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
