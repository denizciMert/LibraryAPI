using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.ShiftDTO; // Importing Shift Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class ShiftsController : ControllerBase
    {
        private readonly ILibraryServiceManager<ShiftGet, ShiftPost, Shift> _libraryServiceManager; // Declaring a private field for the library service manager

        public ShiftsController(ILibraryServiceManager<ShiftGet, ShiftPost, Shift> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
        }

        // GET: api/Shifts
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all shifts
        public async Task<ActionResult<IEnumerable<ShiftGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all shifts

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved shifts
        }

        // GET: api/Shifts/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a shift by its ID
        public async Task<ActionResult<ShiftGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the shift by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved shift
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a shift by its ID
        public async Task<IActionResult> Put(int id, ShiftPost shift)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, shift); // Calling the service to update the shift

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated shift
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPost("Post")] // Defining a POST endpoint to create a new shift
        public async Task<ActionResult<ShiftPost>> Post(ShiftPost shift)
        {
            var result = await _libraryServiceManager.AddAsync(shift); // Calling the service to add a new shift
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created shift
        }

        // DELETE: api/Shifts/5
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a shift by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the shift

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
