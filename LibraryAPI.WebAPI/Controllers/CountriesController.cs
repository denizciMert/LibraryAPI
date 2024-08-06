using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.CountryDTO; // Importing Country Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class CountriesController : ControllerBase
    {
        private readonly ILibraryServiceManager<CountryGet, CountryPost, Country> _libraryServiceManager; // Declaring a private field for the library service manager

        public CountriesController(ILibraryServiceManager<CountryGet, CountryPost, Country> libraryServiceManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
        }

        // GET: api/Countries
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all countries
        public async Task<ActionResult<IEnumerable<CountryGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all countries

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved countries
        }

        // GET: api/Countries/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a country by its ID
        public async Task<ActionResult<CountryGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the country by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved country
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a country by its ID
        public async Task<IActionResult> Put(int id, CountryPost country)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, country); // Calling the service to update the country

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated country
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new country
        public async Task<ActionResult<CountryPost>> Post(CountryPost country)
        {
            var result = await _libraryServiceManager.AddAsync(country); // Calling the service to add a new country
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created country
        }

        // DELETE: api/Countries/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a country by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the country

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
