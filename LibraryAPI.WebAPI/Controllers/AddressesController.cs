using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.AddressDTO; // Importing Address Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class AddressesController : ControllerBase
    {
        private readonly ILibraryServiceManager<AddressGet, AddressPost, Address> _libraryServiceManager; // Declaring a private field for the library service manager
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the account manager

        public AddressesController(
            ILibraryServiceManager<AddressGet, AddressPost, Address> libraryServiceManager,
            ILibraryAccountManager accountManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
            _accountManager = accountManager; // Initializing the account manager through dependency injection
        }

        // GET: api/Addresses
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all addresses
        public async Task<ActionResult<IEnumerable<AddressGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all addresses

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved addresses
        }

        // GET: api/Addresses/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve an address by its ID
        public async Task<ActionResult<AddressGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the address by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved address
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update an address by its ID
        public async Task<IActionResult> Put(int id, AddressPost address)
        {
            var username = User.FindFirst("UserName")!.Value; // Getting the current username from the claims
            var user = await _accountManager.FindUserByUserName(username); // Finding the user by username
            address.UserId = user.Data.Id; // Setting the user ID in the address DTO

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Engellenmiş kullanıcılar adreslerini güncelleyemez."); // Returning a bad request with the error message
            }

            var result = await _libraryServiceManager.UpdateAsync(id, address); // Calling the service to update the address

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated address
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new address
        public async Task<ActionResult<AddressPost>> Post(AddressPost address)
        {
            var username = User.FindFirst("UserName")!.Value; // Getting the current username from the claims
            var user = await _accountManager.FindUserByUserName(username); // Finding the user by username
            address.UserId = user.Data.Id; // Setting the user ID in the address DTO

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Engellenmiş kullanıcılar adreslerini güncelleyemez."); // Returning a bad request with the error message
            }

            var result = await _libraryServiceManager.AddAsync(address); // Calling the service to add a new address
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created address
        }

        // DELETE: api/Addresses/5
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete an address by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _libraryServiceManager.DeleteAsync(id); // Calling the service to delete the address

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
