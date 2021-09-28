using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task InsertAsync(Book book);
        Task<IEnumerable<Book>> GetAllAsync(int? page);
        Task<Book> GetAuthorByIdAsync(int? id);
        Task<IEnumerable<Book>> GetByAuthorAuthenticatedAsync(string authorUserName);
        Task<IEnumerable<Book>> GetByGenre(int enumValue, int? page);
        Task<IEnumerable<Book>> FindByTitleAsync(string title, int? page);
        Task<IEnumerable<Book>> FindByAuthorAsync(string authorName, int? page);
        Task UpdateAsync(Book book);
        Task RemoveAsync(int id);

        Task<int> GetPageCountAsync();
    }
}
