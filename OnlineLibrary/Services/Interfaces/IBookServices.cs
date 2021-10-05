using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IBookServices
    {
        Task<IEnumerable<Book>> ReturnBookListByUserPurchasesAsync();
    }
}
