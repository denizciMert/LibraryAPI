using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.TitleDTO; // Importing Title Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class TitlesController : ControllerBase
    {
        private readonly ILibraryServiceManager<TitleGet, TitlePost, Title> _libraryServiceManager; // Declaring a private field for the library service manager

        public TitlesController(ILibraryServiceManager<TitleGet, TitlePost, Title> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
        }

        // GET: api/Titles
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all titles
        public async Task<ActionResult<IEnumerable<TitleGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all titles

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved titles
        }

        // GET: api/Titles/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a title by its ID
        public async Task<ActionResult<TitleGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the title by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved title
        }

        // PUT: api/Titles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a title by its ID
        public async Task<IActionResult> Put(int id, TitlePost title)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, title); // Calling the service to update the title

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated title
        }

        // POST: api/Titles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPost("Post")] // Defining a POST endpoint to create a new title
        public async Task<ActionResult<TitlePost>> Post(TitlePost title)
        {
            var result = await _libraryServiceManager.AddAsync(title); // Calling the service to add a new title
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created title
        }

        // DELETE: api/Titles/5
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a title by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the title

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
