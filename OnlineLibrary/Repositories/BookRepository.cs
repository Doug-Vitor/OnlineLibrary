using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Extensions;
using OnlineLibrary.Models;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using OnlineLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class BookRepository : AbstractRepository, IBookRepository
    {
        private readonly HttpContextExtensions _contextExtensions;
        private readonly IImageManagerServices _imageManagerServices;

        public BookRepository(AppDbContext context, HttpContextExtensions contextExtensions,
            IImageManagerServices imageManagerServices) : base(context)
        {
            _contextExtensions = contextExtensions;
            _imageManagerServices = imageManagerServices;
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

            Book book = await _context.Books.Where(book => book.Id == id)
                .Include(book => book.Genre).Include(book => book.Author).FirstOrDefaultAsync();
            if (book is null)
                throw new NotFoundException("Não foi possível encontrar um livro correspondente ao ID fornecido.");

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(int? page)
        {
            int booksToSkip = ((page ?? 1) - 1) * 15;
            return await _context.Books.Include(book => book.Genre).Include(bk => bk.Author)
                .OrderBy(book => book.Title).Skip(booksToSkip).Take(15).ToListAsync();
        }

        public async Task<Book> GetByAuthorIdAsync(int? authorId)
        {
            if (authorId is null)
                throw new IdNotProvidedException("ID não informado");

            Book book = await _context.Books.OrderBy(book => book.Title)
                .Where(book => book.Author.Id == authorId).Include(book => book.Genre)
                .Include(bk => bk.Author)
                .FirstOrDefaultAsync();
            if (book is null)
                throw new NotFoundException("Não foi possível encontrar um livro correspondente ao ID informado.");

            return book;
        }

        public async Task<IEnumerable<Book>> GetByGenre(int genreId, int? page)
        {
            int booksToSkip = ((page ?? 1) - 1) * 15;

            return await _context.Books.OrderBy(book => book.Title)
                .Where(bk => bk.Genre.Id == genreId).Include(bk => bk.Author)
                .Include(book => book.Genre).Skip(booksToSkip).Take(15).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByAuthorAuthenticatedAsync()
        {
            string authenticatedUserId = _contextExtensions.GetAuthenticatedUserId();
            return await _context.Books
                .Where(bk => bk.Author.IdentityUser.Id == authenticatedUserId).ToListAsync();
        }

        public async Task<IEnumerable<Book>> FindByStringParamsAsync(string searchString, int? page)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                throw new StringNotProvidedException("Nenhum parâmetro de pesquisa foi informado.");

            int booksToSkip = ((page ?? 1) - 1) * 15;

            List<Book> books = await FindByTitleAsync(searchString, booksToSkip) as List<Book>;
            books.AddRange(await FindByAuthorAsync(searchString, booksToSkip));

            IEnumerable<Book> booksWithoutDuplicates = books.Distinct().ToList();
            return booksWithoutDuplicates;
        }

        private async Task<IEnumerable<Book>> FindByTitleAsync(string searchString, int booksToSkip)
        {
            searchString = searchString.ToLower();
            return await _context.Books.OrderBy(book => book.Title).Skip(booksToSkip).Take(15)
                .Where(book => book.Title.ToLower().Contains(searchString))
                .Include(book => book.Author).Include(book => book.Genre).ToListAsync();
        }

        private async Task<IEnumerable<Book>> FindByAuthorAsync(string searchString, int booksToSkip)
        {
            searchString = searchString.ToLower();
            return await _context.Books.Include(book => book.Author)
                .Include(book => book.Genre).OrderBy(book => book.Author.FullName)
                .Skip(booksToSkip).Take(15)
                .Where(book => book.Author.FullName.ToLower().Contains(searchString)).ToListAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            if (_imageManagerServices.EnsureImageBookExists(book.Id) == false)
                book.ImagePath = "~/Images/BookImages/Default.png";

            _context.Update(book);
            await _context.SaveChangesAsync();
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
    }
}
