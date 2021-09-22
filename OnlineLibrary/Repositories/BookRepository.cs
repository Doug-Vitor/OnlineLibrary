using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data;
using OnlineLibrary.Models;
using OnlineLibrary.Models.Enums;
using OnlineLibrary.Repositories.Exceptions;
using OnlineLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync(int? page)
        {
            int booksToSkip = ((page ?? 1) - 1) * 15;
            return await _context.Books.Include(bk => bk.Author).Skip(booksToSkip).Take(15)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int? id)
        {
            if (id is null)
                throw new IdNotProvidedException("ID não informado");

            Book book = await _context.Books.Where(bk => bk.Id == id).Include(bk => bk.Author)
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

        public async Task<IEnumerable<Book>> GetByAuthorAuthenticatedAsync(string authorUserName)
        {
            return await _context.Books
                .Where(bk => bk.Author.IdentityUser.UserName == authorUserName).ToListAsync();
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
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
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
