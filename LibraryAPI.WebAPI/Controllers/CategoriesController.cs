using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.DTOs.CategoryDTO; // Importing Category Data Transfer Objects
using LibraryAPI.Entities.Models; // Importing entity models
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class CategoriesController : ControllerBase
    {
        private readonly ILibraryServiceManager<CategoryGet, CategoryPost, Category> _libraryServiceManager; // Declaring a private field for the library service manager

        public CategoriesController(ILibraryServiceManager<CategoryGet, CategoryPost, Category> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
        }

        // GET: api/Categories
        [Authorize] // Specifying that this action requires authorization
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all categories
        public async Task<ActionResult<IEnumerable<CategoryGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all categories

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved categories
        }

        // GET: api/Categories/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a category by its ID
        public async Task<ActionResult<CategoryGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the category by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved category
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a category by its ID
        public async Task<IActionResult> Put(int id, CategoryPost category)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, category); // Calling the service to update the category

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated category
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new category
        public async Task<ActionResult<CategoryPost>> Post(CategoryPost category)
        {
            var result = await _libraryServiceManager.AddAsync(category); // Calling the service to add a new category
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created category
        }

        // DELETE: api/Categories/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a category by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the category

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
