using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LibraryAPI.BLL.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _imagesFolderPath;
        public FileUploadService()
        {
            _imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            if (!Directory.Exists(_imagesFolderPath))
            {
                Directory.CreateDirectory(_imagesFolderPath);
            }
        }

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
                return ServiceResult<string>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
