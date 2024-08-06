using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.BLL.Services
{
    /// <summary>
    /// FileUploadService class implements the IFileUploadService interface and provides
    /// functionalities related to file upload management.
    /// </summary>
    public class FileUploadService : IFileUploadService
    {
        // Private field to hold the path to the images folder.
        private readonly string _imagesFolderPath;

        /// <summary>
        /// Constructor to initialize the FileUploadService and create the images folder if it doesn't exist.
        /// </summary>
        public FileUploadService()
        {
            _imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(_imagesFolderPath))
            {
                Directory.CreateDirectory(_imagesFolderPath);
            }
        }

        /// <summary>
        /// Uploads an image file to the server and returns the relative path to the uploaded file.
        /// </summary>
        /// <param name="imageFile">The image file to be uploaded.</param>
        /// <returns>A ServiceResult containing the relative path to the uploaded file.</returns>
        public async Task<ServiceResult<string>> UploadImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return ServiceResult<string>.FailureResult("Yüklenecek görsel bulunamadı.");
            }

            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(_imagesFolderPath, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("Images", fileName);
                return ServiceResult<string>.SuccessResult(relativePath);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }
    }
}
