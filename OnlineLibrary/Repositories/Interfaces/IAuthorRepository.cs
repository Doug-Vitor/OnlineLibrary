using OnlineLibrary.Models;
using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task InsertAsync(Author author);
        Task<Author> GetByIdAsync(int? id);
        Task UpdateAsync(Author author);
        Task RemoveAsync(int id);
    }
}
