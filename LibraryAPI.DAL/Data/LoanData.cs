using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for data operations
using LibraryAPI.Entities.DTOs.LoanDTO; // Importing the DTOs for Loan
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IQueryBase interface for Loan-related data operations
    public class LoanData(ApplicationDbContext context) : IQueryBase<Loan>
    {
        // Selecting all loans with filters applied asynchronously
        public async Task<List<Loan>> SelectAllFiltered()
        {
            return await context.Loans!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser) // Including Member and their ApplicationUser details
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser) // Including Employee and their ApplicationUser details
                .Include(x => x.Book) // Including Book details
                .Where(x => x.State != State.Silindi) // Filtering out loans with state "Silindi"
                .Where(x => x.Active == true) // Filtering only active loans
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting all loans asynchronously
        public async Task<List<Loan>> SelectAll()
        {
            return await context.Loans!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser) // Including Member and their ApplicationUser details
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser) // Including Employee and their ApplicationUser details
                .Include(x => x.Book) // Including Book details
                .ToListAsync(); // Executing the query and returning the result as a list
        }

        // Selecting a loan by ID asynchronously
        public async Task<Loan> SelectForEntity(int id)
        {
            return (await context.Loans!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser) // Including Member and their ApplicationUser details
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser) // Including Employee and their ApplicationUser details
                .Include(x => x.Book) // Including Book details
                .FirstOrDefaultAsync(x => x.Book!.Id == id))!; // Finding the first loan with the specified Book ID
        }

        // Selecting a loan for a user by user ID asynchronously
        public async Task<Loan> SelectForUser(string id)
        {
            return (await context.Loans!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser) // Including Member and their ApplicationUser details
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser) // Including Employee and their ApplicationUser details
                .Include(x => x.Book) // Including Book details
                .FirstOrDefaultAsync(x => x.LoanedMemberId == id))!; // Finding the first loan with the specified LoanedMember ID
        }

        // Selecting a loan by loan ID asynchronously
        public async Task<Loan> SelectForLoan(int id)
        {
            return (await context.Loans!
                .Include(x => x.Member).ThenInclude(x => x!.ApplicationUser) // Including Member and their ApplicationUser details
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser) // Including Employee and their ApplicationUser details
                .Include(x => x.Book) // Including Book details
                .FirstOrDefaultAsync(x => x.Id == id))!; // Finding the first loan with the specified loan ID
        }

        // Checking if a loan is already registered asynchronously
        public async Task<bool> IsRegistered(LoanPost tPost)
        {
            var selectall = await SelectAll(); // Getting all loans
            var loans = selectall.Where(x => x.State != State.Silindi); // Filtering out loans with state "Silindi"
            foreach (var loan in loans)
            {
                if (loan.BookId == tPost.BookId &&
                    loan.LoanedMemberId == tPost.MemberId &&
                    loan.Active) // Checking if a loan is registered with the same Book ID, LoanedMember ID, and is active
                {
                    return true; // Loan is already registered
                }
            }
            return false; // Loan is not registered
        }

        // Checking if a book can be taken for loan and updating the storage information asynchronously
        public async Task<bool> CanTakeLoanAndCalculateStorage(int bookId, int bookCopyNo)
        {
            var book = await context.Books!.Include(x => x.BookCopies).FirstOrDefaultAsync(x => x.Id == bookId); // Finding the book with the specified Book ID and including its copies

            var copies = book!.BookCopies!.ToList(); // Getting the list of book copies

            foreach (var copy in copies)
            {
                if (copy.CopyNo == bookCopyNo)
                {
                    if (copy.Reserved == false) // Checking if the book copy is not reserved
                    {
                        copy.Reserved = true; // Reserving the book copy
                        book.BookCopies = copies.Where(x => x.Reserved == false).ToList(); // Updating the list of available book copies
                        book.CopyCount = (short)book.BookCopies.Count; // Updating the copy count
                        await context.SaveChangesAsync(); // Saving changes to the context
                        return true; // Book can be taken for loan
                    }
                    else
                    {
                        book.BookCopies = copies.Where(x => x.Reserved == false).ToList(); // Updating the list of available book copies
                        book.CopyCount = (short)book.BookCopies.Count; // Updating the copy count
                        await context.SaveChangesAsync(); // Saving changes to the context
                        return false; // Book cannot be taken for loan
                    }
                }
            }
            return false; // Book cannot be taken for loan
        }

        // Changing the reserved status of a book asynchronously
        public async Task ChangeReserveBook(int bookId, int copyNo)
        {
            var book = await context.Books!.Include(x => x.BookCopies).FirstOrDefaultAsync(x => x.Id == bookId); // Finding the book with the specified Book ID and including its copies
            book!.BookCopies!.FirstOrDefault(x => x.CopyNo == copyNo)!.Reserved = false; // Changing the reserved status of the specified book copy to false
            await context.SaveChangesAsync(); // Saving changes to the context
        }

        // Adding a loan to the context
        public void AddToContext(Loan loan)
        {
            context.Loans!.Add(loan); // Adding the loan to the Loans DbSet
        }

        // Saving changes to the context asynchronously
        public async Task SaveContext()
        {
            await context.SaveChangesAsync(); // Saving changes to the context
        }
    }
}
