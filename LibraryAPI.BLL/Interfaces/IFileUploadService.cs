using LibraryAPI.BLL.Core; // Importing the core functionalities of the BLL
using Microsoft.AspNetCore.Http; // Importing the HTTP functionalities, including IFormFile

namespace LibraryAPI.BLL.Interfaces
{
    // Interface for file upload service
    public interface IFileUploadService
    {
        // Method to upload an image file and return the result as a ServiceResult with the file path as a string
        Task<ServiceResult<string>> UploadImage(IFormFile? imageFile);
    }
}