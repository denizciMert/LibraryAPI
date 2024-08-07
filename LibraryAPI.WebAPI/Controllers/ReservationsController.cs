using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.ReservationDTO; // Importing Reservation Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class ReservationsController : ControllerBase
    {
        private readonly ILibraryServiceManager<ReservationGet, ReservationPost, Reservation> _libraryServiceManager; // Declaring a private field for the library service manager
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the library account manager

        public ReservationsController(
            ILibraryServiceManager<ReservationGet, ReservationPost, Reservation> libraryServiceManager,
            ILibraryAccountManager accountManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
            _accountManager = accountManager; // Initializing the account manager through dependency injection
        }

        // GET: api/Reservations
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all reservations
        public async Task<ActionResult<IEnumerable<ReservationGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all reservations

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved reservations
        }

        // GET: api/Reservations/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a reservation by its ID
        public async Task<ActionResult<ReservationGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the reservation by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved reservation
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, ReservationPost reservation)
        {
            var result = await _libraryServiceManager.UpdateAsync(id, reservation);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }*/

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new reservation
        public async Task<ActionResult<ReservationPost>> Post(ReservationPost reservation)
        {
            var user = await _accountManager.FindUserById(reservation.MemberId); // Finding the user by ID
            var userLoans = await _accountManager.GetReservations(user.Data); // Getting the user's reservations
            if (userLoans.Success) // Checking if the user already has a reservation
            {
                return BadRequest("Bu kullanıcının zaten bir rezervasyonu var."); // Returning a bad request if the user already has a reservation
            }

            var employeeUserName = User.FindFirst("UserName")?.Value; // Getting the employee's username from the claims
            var employee = await _accountManager.FindUserByUserName(employeeUserName!); // Finding the employee by username
            reservation.EmployeeId = employee.Data.Id; // Setting the employee ID in the reservation

            var result = await _libraryServiceManager.AddAsync(reservation); // Adding the reservation
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created reservation
        }

        // DELETE: api/Reservations/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a reservation by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _libraryServiceManager.GetByIdAsync(id); // Getting the reservation by ID
            var user = await _accountManager.FindUserByUserName(reservation.Data.UserName!); // Finding the user by username
            var result = await _accountManager.EndReservations(user.Data, id); // Ending the user's reservation
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            await _libraryServiceManager.DeleteAsync(id); // Deleting the reservation
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
