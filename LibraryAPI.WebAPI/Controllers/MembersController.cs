using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.MemberDTO; // Importing Member Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class MembersController : ControllerBase
    {
        private readonly ILibraryUserManager<MemberGet, MemberPost, Member> _libraryUserManager; // Declaring a private field for the library user manager
        private readonly IFileUploadService _fileUploadService; // Declaring a private field for the file upload service

        public MembersController(ILibraryUserManager<MemberGet, MemberPost, Member> libraryUserManager, IFileUploadService fileUploadService)
        {
            _libraryUserManager = libraryUserManager; // Initializing the library user manager through dependency injection
            _fileUploadService = fileUploadService; // Initializing the file upload service through dependency injection
        }

        // GET: api/Members
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all members
        public async Task<ActionResult<IEnumerable<MemberGet>>> GetAll()
        {
            var result = await _libraryUserManager.GetAllAsync(); // Calling the service to get all members

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved members
        }

        // GET: api/Members/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a member by their ID
        public async Task<ActionResult<MemberGet>> Get(string id)
        {
            var result = await _libraryUserManager.GetByIdAsync(id); // Calling the service to get the member by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved member
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update a member by their ID
        public async Task<IActionResult> Put(string id, MemberPost member)
        {
            if (member.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(member.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                member.UserImagePath = imagePath.Data; // Setting the image path in the member DTO
            }

            var result = await _libraryUserManager.UpdateAsync(id, member); // Calling the service to update the member

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the updated member
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Kullanıcı,Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new member
        public async Task<ActionResult<MemberPost>> Post(MemberPost member)
        {
            if (member.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(member.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                member.UserImagePath = imagePath.Data; // Setting the image path in the member DTO
            }
            else
            {
                member.UserImagePath = null; // Setting the image path to null if no file is provided
            }

            member.UserRoleId = 0; // Setting the user role ID to 0
            var result = await _libraryUserManager.AddAsync(member); // Calling the service to add a new member

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created member
        }

        // DELETE: api/Members/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a member by their ID
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _libraryUserManager.DeleteAsync(id); // Calling the service to delete the member

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
