using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.Enums; // Importing enums from the entities
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes
using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class BanController : ControllerBase
    {
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the account manager
        private readonly IConfiguration _configuration; // Declaring a private field for the configuration

        public BanController(
            ILibraryAccountManager accountManager,
            IConfiguration configuration)
        {
            _accountManager = accountManager; // Initializing the account manager through dependency injection
            _configuration = configuration; // Initializing the configuration through dependency injection
        }

        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Ban")] // Defining a POST endpoint to ban a user
        public async Task<ActionResult> Ban(string userName)
        {
            var user = await _accountManager.FindUserByUserName(userName); // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole == UserRole.Yönetici) // Checking if the user is an administrator
            {
                return BadRequest("Yönetici kullanıcı engellenemez."); // Returning a bad request with the error message
            }

            if (user.Data.UserRole == UserRole.Çalışan) // Checking if the user is an employee
            {
                if (User.FindFirst("UserName")?.Value != _configuration["Sudo:Username"]) // Checking if the current user is not the sudo user
                {
                    return BadRequest("Sadece kullanıcıları engelleyebilirsiniz."); // Returning a bad request with the error message
                }
            }

            var result = await _accountManager.BanUser(user.Data); // Banning the user
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the ban
        }

        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("UnBan")] // Defining a POST endpoint to unban a user
        public async Task<ActionResult> UnBan(string userName)
        {
            var user = await _accountManager.FindUserByUserName(userName); // Finding the user by username
            if (!user.Success) // Checking if the operation was unsuccessful
            {
                return NotFound(user.ErrorMessage); // Returning a not found response with the error message
            }

            if (user.Data.UserRole == UserRole.Yönetici) // Checking if the user is an administrator
            {
                return BadRequest("Yönetici kullanıcı zaten engellenmedi."); // Returning a bad request with the error message
            }

            if (user.Data.UserRole == UserRole.Çalışan) // Checking if the user is an employee
            {
                if (User.FindFirst("UserName")?.Value != _configuration["Sudo:Username"]) // Checking if the current user is not the sudo user
                {
                    return BadRequest("Çalışan engellemesini sadece yönetici kaldırabilir."); // Returning a bad request with the error message
                }
            }

            var result = await _accountManager.UnBanUser(user.Data); // Unbanning the user
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the unban
        }
    }
}
