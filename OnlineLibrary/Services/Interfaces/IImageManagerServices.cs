using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OnlineLibrary.Services.Interfaces
{
    public interface IImageManagerServices
    {
        Task<string> UploadBookImageAsync(IFormFile formFile, int bookId);
        Task<string> UploadProfileImageAsync(IFormFile formFile, int authorId);
        bool EnsureImageBookExists(int bookId);
        bool EnsureProfilePhotoExists(int authorId);
    }
}
