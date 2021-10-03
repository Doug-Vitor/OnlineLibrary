using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class AuthorRepository : AbstractRepository, IAuthorRepository
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public AuthorRepository(AppDbContext context, IWebHostEnvironment hostEnvironment)
            : base(context)
        {
            _hostEnvironment = hostEnvironment;
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
            if (EnsureFileExists(author.Id) == false)
                author.ImagePath = "~/Images/ProfilePhotos/Default.png";

            _context.Update(author);
            await _context.SaveChangesAsync();
        }

        private bool EnsureFileExists(int authorId)
        {
            string imagePath = Path.Combine(_hostEnvironment.WebRootPath,
                $@"Images\ProfilePhotos\{authorId}.png");
            if (File.Exists(imagePath))
                return true;

            return false;
        }

        public async Task RemoveAsync(int id)
        {
            Author author = await GetByIdAsync(id);
            _context.Remove(author);
            _context.SaveChangesAsync();
        }
    }
}
