using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.DTOs.EmployeeDTO; // Importing Employee Data Transfer Objects
using LibraryAPI.Entities.Models; // Importing entity models
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class EmployeesController : ControllerBase
    {
        private readonly ILibraryUserManager<EmployeeGet, EmployeePost, Employee> _libraryUserManager; // Declaring a private field for the library user manager
        private readonly IFileUploadService _fileUploadService; // Declaring a private field for the file upload service

        public EmployeesController(ILibraryUserManager<EmployeeGet, EmployeePost, Employee> libraryUserManager, IFileUploadService fileUploadService)
        {
            _libraryUserManager = libraryUserManager; // Initializing the library user manager through dependency injection
            _fileUploadService = fileUploadService; // Initializing the file upload service through dependency injection
        }

        // GET: api/Employees
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all employees
        public async Task<ActionResult<IEnumerable<EmployeeGet>>> GetAll()
        {
            var result = await _libraryUserManager.GetAllAsync(); // Calling the service to get all employees

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved employees
        }

        // GET: api/Employees/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve an employee by their ID
        public async Task<ActionResult<EmployeeGet>> Get(string id)
        {
            var result = await _libraryUserManager.GetByIdAsync(id); // Calling the service to get the employee by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved employee
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPut("Put/{id}")] // Defining a PUT endpoint to update an employee by their ID
        public async Task<IActionResult> Put(string id, EmployeePost employee)
        {
            if (employee.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(employee.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                employee.UserImagePath = imagePath.Data; // Setting the image path in the employee DTO
            }

            var result = await _libraryUserManager.UpdateAsync(id, employee); // Calling the service to update the employee
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the updated employee
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpPost("Post")] // Defining a POST endpoint to create a new employee
        public async Task<ActionResult<EmployeePost>> Post(EmployeePost employee)
        {
            if (employee.FileForm != null) // Checking if a file is provided
            {
                var imagePath = await _fileUploadService.UploadImage(employee.FileForm); // Uploading the image
                if (!imagePath.Success) // Checking if the upload was unsuccessful
                {
                    return BadRequest(imagePath.ErrorMessage); // Returning a bad request with the error message
                }
                employee.UserImagePath = imagePath.Data; // Setting the image path in the employee DTO
            }
            else
            {
                employee.UserImagePath = null; // Setting the image path to null if no file is provided
            }

            var result = await _libraryUserManager.AddAsync(employee); // Calling the service to add a new employee
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the created employee
        }

        // DELETE: api/Employees/5
        [Authorize(Roles = "Yönetici")] // Specifying that this action requires authorization and is restricted to the "Yönetici" role
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete an employee by their ID
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _libraryUserManager.DeleteAsync(id); // Calling the service to delete the employee

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
