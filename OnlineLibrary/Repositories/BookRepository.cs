using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Models.Enums;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class BookRepository : AbstractRepository, IBookRepository
    {
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookRepository(AppDbContext context, HttpContextExtensions contextExtensions,
            IWebHostEnvironment hostEnvironment)
            : base(context)
        {
            _contextExtensions = contextExtensions;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InsertAsync(Book book)
        {
            book.ImagePath = "~/Images/BookImages/Default.png";
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetByIdAsync(int? id)
        {
            if (id is null)
                throw new IdNotProvidedException("ID não informado.");

            Book book = await _context.Books.Where(book => book.Id == id).FirstOrDefaultAsync();
            if (book is null)
                throw new NotFoundException("Não foi possível encontrar um livro correspondente ao ID fornecido.");

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(int? page)
        {
            int booksToSkip = ((page ?? 1) - 1) * 15;
            return await _context.Books.Include(bk => bk.Author).Skip(booksToSkip).Take(15)
                .ToListAsync();
        }

        public async Task<Book> GetByAuthorIdAsync(int? authorId)
        {
            if (id is null)
                throw new IdNotProvidedException("ID não informado");

            Book book = await _context.Books.Where(book => book.Author.Id == id).Include(bk => bk.Author)
                .FirstOrDefaultAsync();
            if (book is null)
                throw new NotFoundException("Não foi possível encontrar um livro correspondente ao ID informado.");

            return book;
        }

        public async Task<IEnumerable<Book>> GetByGenre(int genreValue, int? page)
        {
            int booksToSkip = ((page ?? 1) - 1) * 15;
            string genreName = Enum.GetName(typeof(Genre), genreValue);
            Genre genre = (Genre)Enum.Parse(typeof(Genre), genreName);

            return await _context.Books.Where(bk => bk.Genre == genre).Include(bk => bk.Author)
                .Skip(booksToSkip).Take(15).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByAuthorAuthenticatedAsync()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.Books
                .Where(bk => bk.Author.IdentityUser.Id == authenticatedUserId).ToListAsync();
        }

        public async Task<IEnumerable<Book>> FindByTitleAsync(string title, int? page)
        {
            return await FindByStringParameterAsync(title, page, true);
        }

        public async Task<IEnumerable<Book>> FindByAuthorAsync(string authorName, int? page)
        {
            return await FindByStringParameterAsync(authorName, page, false);
        }

        private async Task<IEnumerable<Book>> FindByStringParameterAsync(string parameter, 
            int? page, bool findByTitle)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new StringNotProvidedException("Nenhum parâmetro de pesquisa foi informado.");

            List<Book> books = new();
            int booksToSkip = ((page ?? 1) - 1) * 15;
            if (findByTitle)
                books = await _context.Books.Where(bk => bk.Title.ToLower()
                    .Contains(parameter.ToLower())).Include(bk => bk.Author).Skip(booksToSkip)
                    .Take(15).ToListAsync();
            else
                books = await _context.Books.Include(bk => bk.Author).Where(bk => 
                    bk.Author.FullName.ToLower().Contains(parameter.ToLower()))
                    .Skip(booksToSkip).Take(15).ToListAsync();

            if (books.Count == 0)
                throw new NotFoundException("Não foi possível encontrar nenhum livro correspondente ao parâmetro de pesquisa informado.");

            return books;
        }

        public async Task UpdateAsync(Book book)
        {
            if (EnsureFileExists(book.Id) == false)
                book.ImagePath = "~/Images/BookImages/Default.png";

            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        private bool EnsureFileExists(int bookId)
        {
            string imagePath = Path.Combine(_hostEnvironment.WebRootPath,
                $@"Images\BookImages\{bookId}.png");
            if (File.Exists(imagePath))
                return true;

            return false;
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                Book book = await GetByIdAsync(id);
                _context.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<int> GetPageCountAsync()
        {
            return (int)Math.Ceiling((double)await _context.Books.CountAsync() / 15);
        }
    }
}
