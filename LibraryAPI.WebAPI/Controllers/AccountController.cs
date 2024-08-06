using Microsoft.AspNetCore.Authorization; // Importing authorization attributes
using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.Enums; // Importing enums from the entities

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class AccountController : ControllerBase
    {
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the account manager

        public AccountController(
            ILibraryAccountManager accountManager)
        {
            _accountManager = accountManager; // Initializing the account manager through dependency injection
        }

        [HttpPost("Login")] // Defining a POST endpoint for login
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = await _accountManager.FindUserByUserName(userName); // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }
            var result = await _accountManager.Login(user.Data, password); // Attempting to log in the user
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the login result
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("Logout")] // Defining a GET endpoint for logout
        public async Task<ActionResult> Logout()
        {
            var result = await _accountManager.Logout(); // Attempting to log out the user
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return Unauthorized(result.ErrorMessage); // Returning an unauthorized response with the error message
            }
            return Ok(result.Data); // Returning a success response with the logout result
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("GetProfile")] // Defining a GET endpoint to retrieve the user's profile
        public Task<ActionResult> GetProfile()
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Task.FromResult<ActionResult>(Unauthorized()); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return Task.FromResult<ActionResult>(NotFound(user.ErrorMessage)); // Returning a not found response with the error message
            }

            return Task.FromResult<ActionResult>(Ok(user.Data)); // Returning a success response with the user's profile
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("GetLoans")] // Defining a GET endpoint to retrieve the user's loans
        public async Task<ActionResult> GetLoans()
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var loans = await _accountManager.GetUserLoans(user.Data); // Getting the user's loans
            if (!loans.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(loans.ErrorMessage); // Returning a not found response with the error message
            }

            return Ok(loans.Data); // Returning a success response with the user's loans
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("ReturnLoans")] // Defining a POST endpoint to return a loan
        public async Task<ActionResult> ReturnLoans(int bookId)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Öncelikle cezanızı ödemelisiniz."); // Returning a bad request with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var loans = await _accountManager.ReturnUserLoans(user.Data, bookId); // Attempting to return the loan
            if (loans.Success == false) // Checking if the operation was unsuccessful
            {
                return NotFound(loans.ErrorMessage); // Returning a not found response with the error message
            }

            return Ok(loans.Data); // Returning a success response with the result of the loan return
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("GetPenalties")] // Defining a GET endpoint to retrieve the user's penalties
        public async Task<ActionResult> GetPenalties()
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var penalties = await _accountManager.GetUserPenalties(user.Data); // Getting the user's penalties
            if (!penalties.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(penalties.ErrorMessage); // Returning a not found response with the error message
            }

            return Ok(penalties.Data); // Returning a success response with the user's penalties
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("PayPenalties")] // Defining a POST endpoint to pay a penalty
        public async Task<ActionResult> PayPenalties(int penltyId, float amount)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var penalty = await _accountManager.PayUserPenalty(user.Data, penltyId, amount); // Attempting to pay the penalty
            if (penalty.Success == false) // Checking if the operation was unsuccessful
            {
                return NotFound(penalty.ErrorMessage); // Returning a not found response with the error message
            }

            return Ok(penalty.Data); // Returning a success response with the result of the penalty payment
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("RateBook")] // Defining a POST endpoint to rate a book
        public async Task<ActionResult> RateBook(int bookId, float rate)
        {
            if (0 > rate || rate > 5) // Checking if the rate is within the valid range
            {
                return BadRequest("Puanlandırma 0 ve 5 arasında olmalı."); // Returning a bad request with the error message
            }

            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Öncelikle cezanızı ödemelisiniz."); // Returning a bad request with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var result = await _accountManager.RateBook(user.Data, bookId, rate); // Attempting to rate the book
            if (result.Success == false) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the result of the rating
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("GetReservation")] // Defining a GET endpoint to retrieve the user's reservation
        public async Task<ActionResult> GetReservation()
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var result = await _accountManager.GetReservations(user.Data); // Getting the user's reservations
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the user's reservations
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("EndReservation")] // Defining a POST endpoint to end a reservation
        public async Task<ActionResult> EndReservation(int reservationId)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole != UserRole.Kullanıcı && user.Data.UserRole != UserRole.Ziyaretçi) // Checking if the user has the correct role
            {
                return BadRequest("Bu işlemi gerçekleştirmek için kullanıcı olmalısınız."); // Returning a bad request with the error message
            }

            var result = await _accountManager.EndReservations(user.Data, reservationId); // Attempting to end the reservation
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of ending the reservation
        }

        [HttpPost("ForgetPassword")] // Defining a POST endpoint to request a password reset
        public async Task<ActionResult<string>> ForgetPassword(string userName)
        {
            var user = _accountManager.FindUserByUserName(userName).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            var token = await _accountManager.ForgetPassword(user.Data); // Requesting a password reset token
            if (!token.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(token.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(token.Data); // Returning a success response with the password reset token
        }

        [HttpPost("ResetPassword")] // Defining a POST endpoint to reset the password
        public async Task<ActionResult> ResetPassword(string token, string newPassword)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            var result = await _accountManager.ResetPassword(user.Data, newPassword, token); // Attempting to reset the password
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the password reset
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("RequestEmailChange")] // Defining a POST endpoint to request an email change
        public async Task<ActionResult<string>> RequestEmailChange(string newEmail)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Öncelikle cezanızı ödemelisiniz."); // Returning a bad request with the error message
            }

            var token = await _accountManager.RequestEmailChange(user.Data, newEmail); // Requesting an email change token
            if (!token.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(token.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(token.Data); // Returning a success response with the email change token
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("ChangeEmail")] // Defining a POST endpoint to change the email
        public async Task<ActionResult<string>> ChangeEmail(string newEmail, string token)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.Banned) // Checking if the user is banned
            {
                return BadRequest("Öncelikle cezanızı ödemelisiniz."); // Returning a bad request with the error message
            }

            var result = await _accountManager.ChangeEmail(user.Data, newEmail, token); // Attempting to change the email
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the email change
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpGet("RequestEmailConfirm")] // Defining a GET endpoint to request email confirmation
        public async Task<ActionResult<string>> RequestEmailConfirm()
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            var token = await _accountManager.RequestEmailConfirm(user.Data); // Requesting an email confirmation token
            if (!token.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(token.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(token.Data); // Returning a success response with the email confirmation token
        }

        [Authorize] // Specifying that this action requires authorization
        [HttpPost("ConfirmEmail")] // Defining a POST endpoint to confirm the email
        public async Task<ActionResult<string>> ConfirmEmail(string token)
        {
            var username = User.FindFirst("UserName")?.Value; // Getting the current username from the claims
            if (username == null) // Checking if the username is null
            {
                return Unauthorized(); // Returning an unauthorized response
            }

            var user = _accountManager.FindUserByUserName(username).Result; // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            var result = await _accountManager.ConfirmEmail(user.Data, token); // Attempting to confirm the email
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the email confirmation
        }
    }
}
