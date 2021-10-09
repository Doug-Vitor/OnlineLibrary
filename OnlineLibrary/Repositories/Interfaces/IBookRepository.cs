using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task InsertAsync(Book book);
        Task<Book> GetByIdAsync(int? id);
        Task<IEnumerable<Book>> GetAllAsync(int? page);
        Task<Book> GetByAuthorIdAsync(int? authorId);
        Task<IEnumerable<Book>> GetByAuthorAuthenticatedAsync();
        Task<IEnumerable<Book>> GetByGenre(int genreId, int? page);
        Task<IEnumerable<Book>> FindByStringParamsAsync(string searchString, int? page);
        Task UpdateAsync(Book book);
        Task RemoveAsync(int id);
    }
}
