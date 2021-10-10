using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IPageCountServices
    {
        Task<int> GetTotalPagesCountAsync();
        Task<int> GetTotalPagesCountWithSearchParametersAsync(string searchString);
        Task<int> GetTotalPagesCountSearchingByGenre(int genreId);
    }
}
