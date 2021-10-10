using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OnlineLibrary.Services
{
    public class ImageManagerServices : IImageManagerServices
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private Dictionary<string, string> ImageValidation = new()
        {
            { "InvalidFileExtension", false.ToString() },
            { "InvalidFileName", false.ToString() },
            { "DestinationFolder", "" },
        };

        public ImageManagerServices(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        private void ValidAndReturnImagePath(bool isProfilePhoto, string fileName)
        {
            if (fileName.ToLower().Contains("default"))
            {
                ImageValidation["InvalidFileName"] = true.ToString();
                return;
            }

            string fileExtension = fileName.Substring(fileName.Length - 4);
            if (fileExtension != ".png")
            {
                ImageValidation["InvalidFileExtension"] = true.ToString();
                return;
            }
            string destinationFolder = _hostEnvironment.WebRootPath;
            if (isProfilePhoto)
                ImageValidation["DestinationFolder"] = Path.Combine(destinationFolder, @"Images\ProfilePhotos");
            else
                ImageValidation["DestinationFolder"] = Path.Combine(destinationFolder, @"Images\BookImages");
        }

        public async Task<string> UploadProfileImageAsync(IFormFile formFile, int authorId)
        {
            if (formFile is null)
                return null;

            ValidAndReturnImagePath(true, formFile.FileName);
            if (ImageValidation["InvalidFileExtension"] == true.ToString())
                return "Ocorreu um erro no upload do arquivo. Apenas arquivos com extensão .png são permitidos.";
            if (ImageValidation["InvalidFileName"] == true.ToString())
                return "Ocorreu um erro no upload do arquivo. Renomeie o arquivo enviado e tente novamente.";

            string filePath = $@"{ImageValidation["DestinationFolder"]}\{authorId}.png";
            using FileStream stream = new(filePath, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return null;
        }

        public async Task<string> UploadBookImageAsync(IFormFile formFile, int bookId)
        {
            if (formFile is null)
                return null;

            ValidAndReturnImagePath(false, formFile.FileName);
            if (ImageValidation["InvalidFileExtension"] == true.ToString())
                return "Ocorreu um erro no upload do arquivo. Apenas arquivos com extensão .png são permitidos.";
            if (ImageValidation["InvalidFileName"] == true.ToString())
                return "Ocorreu um erro no upload do arquivo. Renomeie o arquivo enviado e tente novamente.";

            string filePath = $@"{ImageValidation["DestinationFolder"]}\{bookId}.png";
            using FileStream stream = new(filePath, FileMode.Create);
            await formFile.CopyToAsync(stream);

            return null;
        }

        public bool EnsureImageBookExists(int bookId)
        {
            string imagePath = Path.Combine(_hostEnvironment.WebRootPath,
                $@"Images\BookImages\{bookId}.png");
            if (File.Exists(imagePath))
                return true;

            return false;
        }

        public bool EnsureProfilePhotoExists(int authorId)
        {
            string imagePath = Path.Combine(_hostEnvironment.WebRootPath,
                $@"Images\ProfilePhotos\{authorId}.png");
            if (File.Exists(imagePath))
                return true;

            return false;
        }
    }
}
