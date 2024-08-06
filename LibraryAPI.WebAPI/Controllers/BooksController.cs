using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.BookDTO; // Importing Book Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class BooksController : ControllerBase
    {
        private readonly ILibraryServiceManager<BookGet, BookPost, Book> _libraryServiceManager; // Declaring a private field for the library service manager
        private readonly IFileUploadService _fileUploadService; // Declaring a private field for the file upload service

        public BooksController(ILibraryServiceManager<BookGet, BookPost, Book> libraryServiceManager, IFileUploadService fileUploadService)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
            _fileUploadService = fileUploadService; // Initializing the file upload service through dependency injection
        }

        // GET: api/Books
        [Authorize] // Specifying that this action requires authorization
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all books
        public async Task<ActionResult<IEnumerable<BookGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all books

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved books
        }

        // GET: api/Books/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a book by its ID
        public async Task<ActionResult<BookGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the book by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved book
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a book by its ID
        public async Task<IActionResult> Put(int id, BookPost book)
        {
            if (book.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(book.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                book.ImagePath = imagePath.Data; // Setting the image path in the book DTO
            }

            var result = await _libraryServiceManager.UpdateAsync(id, book); // Calling the service to update the book

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated book
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new book
        public async Task<ActionResult<BookPost>> Post(BookPost book)
        {
            if (book.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(book.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                book.ImagePath = imagePath.Data; // Setting the image path in the book DTO
            }
            else
            {
                book.ImagePath = null; // Setting the image path to null if no file is provided
            }

            var result = await _libraryServiceManager.AddAsync(book); // Calling the service to add a new book
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created book
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a book by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the book

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
