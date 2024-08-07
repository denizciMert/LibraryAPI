using Microsoft.AspNetCore.Mvc; // Importing MVC functionalities
using LibraryAPI.Entities.Models; // Importing entity models
using LibraryAPI.BLL.Interfaces; // Importing interfaces from the Business Logic Layer
using LibraryAPI.Entities.DTOs.BookDTO; // Importing Book Data Transfer Objects
using LibraryAPI.Entities.DTOs.LoanDTO; // Importing Loan Data Transfer Objects
using Microsoft.AspNetCore.Authorization; // Importing authorization attributes

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]/")] // Setting the route for the controller
    [ApiController] // Specifying that this is an API controller
    public class LoansController : ControllerBase
    {
        private readonly ILibraryServiceManager<LoanGet, LoanPost, Loan> _libraryServiceManager; // Declaring a private field for the library service manager
        private readonly ILibraryServiceManager<BookGet, BookPost, Book> _libraryBookManager; // Declaring a private field for the library book manager
        private readonly ILibraryAccountManager _accountManager; // Declaring a private field for the library account manager

        public LoansController(
            ILibraryServiceManager<LoanGet, LoanPost, Loan> libraryServiceManager,
            ILibraryServiceManager<BookGet, BookPost, Book> libraryBookManager,
            ILibraryAccountManager accountManager)
        {
            _libraryServiceManager = libraryServiceManager; // Initializing the library service manager through dependency injection
            _libraryBookManager = libraryBookManager; // Initializing the library book manager through dependency injection
            _accountManager = accountManager; // Initializing the account manager through dependency injection
        }

        // GET: api/Loans
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get")] // Defining a GET endpoint to retrieve all loans
        public async Task<ActionResult<IEnumerable<LoanGet>>> GetAll()
        {
            var result = await _libraryServiceManager.GetAllAsync(); // Calling the service to get all loans

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved loans
        }

        // GET: api/Loans/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpGet("Get/{id}")] // Defining a GET endpoint to retrieve a loan by its ID
        public async Task<ActionResult<LoanGet>> Get(int id)
        {
            var result = await _libraryServiceManager.GetByIdAsync(id); // Calling the service to get the loan by ID

            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the retrieved loan
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[Authorize(Roles = "Çalışan,Yönetici")]
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> Put(int id, LoanPost loan)
        {
            var user = await _accountManager.FindUserById(loan.MemberId);
            var userLoans = await _accountManager.GetUserLoans(user.Data);
            if (userLoans.Data.Count!=0)
            {
                return BadRequest("Bu kullanıcı zaten bir işlem yapmış.");
            }

            var updatedLoan = userLoans.Data.FirstOrDefault(x => x.Id == id);
            var updatedLoanUser = await _accountManager.FindUserByUserName(updatedLoan!.MemberUserName!);
            var books = await _libraryBookManager.GetAllAsync();
            var book = books.Data.FirstOrDefault(x => x.Isbn == updatedLoan.BookIsbn);

            if (loan.CopyNo==updatedLoan.CopyNo &&
                loan.BookId == book!.Id &&
                loan.MemberId == updatedLoanUser.Data.UserName)
            {
                return Ok("Girdiğiniz değerler aynı. Değiştirilecek veri yok.");
            }

            var resetLoan= await _accountManager.ReturnUserLoans(updatedLoanUser.Data, book!.Id);
            if (resetLoan.Success == false)
            {
                return NotFound(resetLoan.ErrorMessage);
            }

            var result = await _libraryServiceManager.UpdateAsync(id, loan);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }*/

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpPost("Post")] // Defining a POST endpoint to create a new loan
        public async Task<ActionResult<LoanPost>> Post(LoanPost loan)
        {
            var user = await _accountManager.FindUserById(loan.MemberId); // Finding the user by ID
            var userLoans = await _accountManager.GetUserLoans(user.Data); // Getting the user's loans
            if (userLoans.Success) // Checking if the user already has a loan
            {
                return BadRequest("Bu kullanıcı zaten bir işlem yapmış."); // Returning a bad request if the user already has a loan
            }

            var employeeUserName = User.FindFirst("UserName")?.Value; // Getting the employee's username from the claims
            var employee = await _accountManager.FindUserByUserName(employeeUserName!); // Finding the employee by username
            loan.EmployeeId = employee.Data.Id; // Setting the employee ID in the loan DTO

            var result = await _libraryServiceManager.AddAsync(loan); // Adding the loan
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }

            return Ok(result.Data); // Returning a success response with the created loan
        }

        // DELETE: api/Loans/5
        [Authorize(Roles = "Çalışan,Yönetici")] // Specifying that this action requires authorization and is restricted to certain roles
        [HttpDelete("Delete/{id}")] // Defining a DELETE endpoint to delete a loan by its ID
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _libraryServiceManager.GetByIdAsync(id); // Getting the loan by ID
            var user = await _accountManager.FindUserByUserName(loan.Data.MemberUserName!); // Finding the user by username
            var books = await _libraryBookManager.GetAllAsync(); // Getting all books
            var book = books.Data.FirstOrDefault(x => x.Isbn == loan.Data.BookIsbn); // Finding the book by ISBN
            var result = await _accountManager.ReturnUserLoans(user.Data, book!.Id); // Returning the user's loans
            if (!result.Success) // Checking if the operation was unsuccessful
            {
                return BadRequest(result.ErrorMessage); // Returning a bad request with the error message
            }
            await _libraryServiceManager.DeleteAsync(id); // Deleting the loan
            return Ok(result.Data); // Returning a success response with the result of the deletion
        }
    }
}
