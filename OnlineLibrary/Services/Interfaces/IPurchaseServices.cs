using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IPurchaseServices
    {
        Task ConvertCartItemsToPurchase();
    }
}
