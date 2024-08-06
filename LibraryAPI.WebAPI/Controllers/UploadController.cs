using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes
using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class UploadController : ControllerBase
    {
        private readonly IFileUploadService _uploadService; // Declaring a private field for the file upload service

        public UploadController(IFileUploadService uploadService)
        {
            _uploadService = uploadService; // Initializing the file upload service through dependency injection
        }

        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Upload")] // Defining a POST endpoint for image upload
        public async Task<IActionResult> UploadImage(IFormFile? imageFile)
        {
            var result = await _uploadService.UploadImage(imageFile); // Calling the upload service to handle the file upload

            if (!result.Success) // Checking if the upload was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the uploaded file data
        }
    }
}
