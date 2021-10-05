using System.Threading.Tasks;

namespace OnlineLibrary.Repositories.Interfaces
{
    public interface IPurchaseServices
    {
        Task SetCartItemsToPurchase();
    }
}
