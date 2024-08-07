using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.PenaltyDTO; // Importing Penalty Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class PenaltiesController : ControllerBase
    {
        private readonly ILibraryServiceManager<PenaltyGet, PenaltyPost, Penalty> _libraryServiceManager; // Declaring a private field for the library service manager
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the library account manager

        public PenaltiesController(
            ILibraryServiceManager<PenaltyGet, PenaltyPost, Penalty> libraryServiceManager,
            ILibraryAccountManager accountManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
            _accountManager = accountManager; // Initializing the account manager through dependency injection
        }

        // GET: api/Penalties
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all penalties
        public async Task<ActionResult<IEnumerable<PenaltyGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all penalties

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved penalties
        }

        // GET: api/Penalties/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a penalty by its ID
        public async Task<ActionResult<PenaltyGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the penalty by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved penalty
        }

        // PUT: api/Penalties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, PenaltyPost penalty)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, penalty);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }*/

        // POST: api/Penalties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new penalty
        public async Task<ActionResult<PenaltyPost>> Post(PenaltyPost penalty)
        {
            var user = await _accountManager.FindUserById(penalty.PenaltiedMemberId); // Finding the user by ID
            var userPenalties = await _accountManager.GetUserPenalties(user.Data); // Getting the user's penalties
            if (userPenalties.Success) // Checking if the user already has a penalty
            {
                return BadRequest("Bu kullanıcının zaten bir cezaya sahip. Eğer hatalı bir kayıt girildiyse öncelikle hatalı kaydı silin."); // Returning a bad request if the user already has a penalty
            }

            var result = await _libraryServiceManager.AddAsync(penalty); // Adding the penalty
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created penalty
        }

        // DELETE: api/Penalties/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a penalty by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var penalty = await _libraryServiceManager.GetByIdAsync(id); // Getting the penalty by ID
            var user = await _accountManager.FindUserByUserName(penalty.Data.UserName); // Finding the user by username
            var result = await _accountManager.PayUserPenalty(user.Data, id, penalty.Data.PenaltyAmount); // Paying the user's penalty
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            await _libraryServiceManager.DeleteAsync(id); // Deleting the penalty
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
