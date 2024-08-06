using LibraryAPI.DAL.Data.Interfaces; // Importing the interfaces for account-related data operations
using LibraryAPI.Entities.Enums; // Importing the enums used in the project
using LibraryAPI.Entities.Models; // Importing the entity models
using Microsoft.AspNetCore.Identity; // Importing the Identity framework
using System.Security.Claims; // Importing claims functionalities
using Microsoft.EntityFrameworkCore; // Importing Entity Framework Core functionalities

namespace LibraryAPI.DAL.Data
{
    // Implementing the IAccountBase interface for account-related data operations
    public class AccountData(
        UserManager<ApplicationUser> userManager, // Injecting UserManager to manage user operations
        SignInManager<ApplicationUser> signInManager, // Injecting SignInManager to manage sign-in operations
        ApplicationDbContext context // Injecting the ApplicationDbContext to interact with the database
        ) : IAccountBase
    {
        // Finding a user by username asynchronously
        public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName); // Finding the user by username
            return user!; // Returning the found user
        }

        // Finding a user by ID asynchronously
        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id); // Finding the user by ID
            return user!; // Returning the found user
        }

        // Signing in a user asynchronously
        public Task<bool> UserSignInAsync(ApplicationUser user, string password)
        {
            return Task.FromResult(signInManager.PasswordSignInAsync(user, password, false, false).Result.Succeeded); // Signing in the user and returning the result
        }

        // Checking the user's password asynchronously
        public async Task<bool> PasswordCheck(ApplicationUser user, string password)
        {
            var result = await userManager.CheckPasswordAsync(user, password); // Checking the user's password
            return result; // Returning the result
        }

        // Signing out the user asynchronously
        public Task<bool> UserSignOutAsync()
        {
            return Task.FromResult(signInManager.SignOutAsync().IsCompletedSuccessfully); // Signing out the user and returning the result
        }

        // Generating a password reset token asynchronously
        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user); // Generating the password reset token
        }

        // Generating an email change token asynchronously
        public async Task<string> GenerateEmailChangeToken(ApplicationUser user, string newEmail)
        {
            return await userManager.GenerateChangeEmailTokenAsync(user, newEmail); // Generating the email change token
        }

        // Generating an email confirmation token asynchronously
        public async Task<string> GenerateEmailConfirmToken(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user); // Generating the email confirmation token
        }

        // Changing the user's password asynchronously
        public Task<bool> PasswordChange(ApplicationUser user, string newPassword, string token)
        {
            return Task.FromResult(userManager.ResetPasswordAsync(user, token, newPassword).Result.Succeeded); // Changing the password and returning the result
        }

        // Changing the user's email asynchronously
        public async Task<bool> EmailChange(ApplicationUser user, string newEmail, string token)
        {
            var result = userManager.ChangeEmailAsync(user, newEmail, token).Result.Succeeded; // Changing the email and returning the result
            await userManager.UpdateNormalizedEmailAsync(user); // Updating the normalized email
            return result; // Returning the result
        }

        // Confirming the user's email asynchronously
        public Task<bool> EmailConfirm(ApplicationUser user, string token)
        {
            return Task.FromResult(userManager.ConfirmEmailAsync(user, token).Result.Succeeded); // Confirming the email and returning the result
        }

        // Checking if the user's email is confirmed asynchronously
        public Task<bool> IsEmailConfirmed(ApplicationUser user)
        {
            return Task.FromResult(userManager.IsEmailConfirmedAsync(user).Result); // Checking if the email is confirmed and returning the result
        }

        // Changing the user's role asynchronously
        public async Task<bool> ChangeUserRole(ApplicationUser user)
        {
            var role0 = 0; // Defining role0
            var role1 = 1; // Defining role1
            var role0Name = ((UserRole)role0).ToString(); // Converting role0 to string
            var role1Name = ((UserRole)role1).ToString(); // Converting role1 to string
            var isUserRole0 = await userManager.IsInRoleAsync(user, role0Name); // Checking if the user is in role0
            if (!isUserRole0) // If the user is not in role0
            {
                var isUserRole1 = await userManager.IsInRoleAsync(user, role1Name); // Checking if the user is in role1
                if (!isUserRole1) // If the user is not in role1
                {
                    return false; // Returning false
                }

                return true; // Returning true
            }
            var removeRole = userManager.RemoveFromRoleAsync(user, role0Name).Result; // Removing the user from role0
            if (!removeRole.Succeeded) // If the removal failed
            {
                return false; // Returning false
            }
            var addRole = userManager.AddToRoleAsync(user, role1Name).Result; // Adding the user to role1
            if (!addRole.Succeeded) // If the addition failed
            {
                return false; // Returning false
            }
            return true; // Returning true
        }

        // Getting the user's roles asynchronously
        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            var result = await userManager.GetRolesAsync(user); // Getting the user's roles
            return result; // Returning the roles
        }

        // Getting the user's claims asynchronously
        public async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            var result = await userManager.GetClaimsAsync(user); // Getting the user's claims
            return result; // Returning the claims
        }

        // Updating the user asynchronously
        public async Task<IdentityResult> UpdateUser(ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user); // Updating the user
            return result; // Returning the result
        }

        // Getting the user's active loans asynchronously
        public async Task<List<Loan>> GetLoans(ApplicationUser user)
        {
            var result = await context.Loans!
                .Include(x => x.Book) // Including related books
                .ThenInclude(x => x!.BookCopies) // Including related book copies
                .Include(x => x.Employee) // Including related employees
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for employees
                .Include(x => x.Member) // Including related members
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for members
                .Where(x => x.Member!.ApplicationUser!.Id == user.Id) // Filtering by the user's ID
                .Where(x => x.Active == true) // Filtering by active loans
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted loans
                .ToListAsync(); // Converting the result to a list
            return result; // Returning the result
        }

        // Getting the user's returned loans
        public Task<List<Loan>> GetReturnedLoans(ApplicationUser user)
        {
            var result = context.Loans!
                .Include(x => x.Book) // Including related books
                .Include(x => x.Employee) // Including related employees
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for employees
                .Include(x => x.Member) // Including related members
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for members
                .Where(x => x.Member!.ApplicationUser!.Id == user.Id) // Filtering by the user's ID
                .Where(x => x.Active == false) // Filtering by inactive loans
                .Where(x => x.State != State.Silindi) // Filtering by non-deleted loans
                .ToList(); // Converting the result to a list
            return Task.FromResult(result); // Returning the result
        }

        // Changing the reserved value for a returned loan asynchronously
        public async Task ChangeReserveValueForReturnedLoan(int bookId, int copyNo)
        {
            var book = await context.Books!.Include(x => x.BookCopies).Where(x => x.State != State.Silindi).FirstOrDefaultAsync(x => x.Id == bookId); // Finding the book by ID and including related book copies
            var copies = book!.BookCopies!.ToList(); // Converting book copies to a list
            var returnedCopy = new BookCopy // Creating a new book copy for the returned copy
            {
                BookId = bookId,
                CopyNo = copyNo,
                Reserved = false,
                State = State.Güncellendi,
                CreationDateLog = book.CreationDateLog,
                UpdateDateLog = DateTime.Now,
                DeleteDateLog = null
            };
            copies.Add(returnedCopy); // Adding the returned copy to the list
            book.BookCopies = copies.Where(x => x.Reserved == false).ToList(); // Filtering by non-reserved copies
            book.CopyCount = (short)book.BookCopies.Count; // Updating the copy count
            context.Books!.Update(book); // Updating the book
            await context.SaveChangesAsync(); // Saving changes to the database
        }

        // Getting the user's penalties
        public Task<List<Penalty>> GetPenalties(ApplicationUser user)
        {
            var result = context.Penalties!
                .Include(x => x.Loan) // Including related loans
                .ThenInclude(x => x!.Book) // Including related books
                .Include(x => x.Loan) // Including related loans
                .ThenInclude(x => x!.Employee) // Including related employees
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for employees
                .Include(x => x.Member) // Including related members
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for members
                .Include(x => x.PenaltyType) // Including related penalty types
                .Where(x => x.Member!.ApplicationUser!.Id == user.Id) // Filtering by the user's ID
                .Where(x => x.Active == true) // Filtering by active penalties
                .ToList(); // Converting the result to a list
            return Task.FromResult(result); // Returning the result
        }

        // Adding a rating and saving changes asynchronously
        public async Task AddRatingAndSave(BookRating rating)
        {
            context.BookRatings!.Add(rating); // Adding the rating
            await context.SaveChangesAsync(); // Saving changes to the database
        }

        // Calculating the rating for a book asynchronously
        public async Task CalculateRating(int bookId)
        {
            var rates = await context.BookRatings!
                .Include(x => x.Book) // Including related books
                .Include(x => x.Member) // Including related members
                .ThenInclude(x => x!.ApplicationUser) // Including related application users for members
                .Where(x => x.Book!.Id == bookId) // Filtering by the book ID
                .Select(x => x.Rate) // Selecting the rating values
                .ToListAsync(); // Converting the result to a list

            var ratesSum = rates.Sum(); // Summing the rating values
            var voteCount = rates.Count; // Counting the votes

            var rating = ratesSum / voteCount; // Calculating the average rating

            var book = await context.Books!.FirstOrDefaultAsync(x => x.Id == bookId); // Finding the book by ID

            book!.Rating = rating; // Updating the book's rating
            context.Books!.Update(book); // Updating the book
            await context.SaveChangesAsync(); // Saving changes to the database
        }

        // Checking if the user has rated the book before and updating the rating if necessary
        public async Task<bool> IsRatedBefore(ApplicationUser user, int bookId, float rating)
        {
            var data = await context.BookRatings!.FirstOrDefaultAsync(x =>
                x.RatedBookId == bookId && x.RaterMemberId == user.Id); // Finding the existing rating

            if (data != null) // If the rating exists
            {
                data.Rate = rating; // Updating the rating value
                context.BookRatings!.Update(data); // Updating the rating
                await context.SaveChangesAsync(); // Saving changes to the database
                await CalculateRating(bookId); // Recalculating the book's rating
                return true; // Returning true
            }
            return false; // Returning false
        }

        // Checking the user's status and applying penalties if necessary
        public async Task CheckUserStatus(ApplicationUser user)
        {
            if (user.Banned == false) // If the user is not banned
            {
                var loans = await context.Loans!
                    .Where(x => x.LoanedMemberId == user.Id) // Filtering by the user's ID
                    .Where(x => x.Active == true) // Filtering by active loans
                    .Where(x => x.State != State.Silindi) // Filtering by non-deleted loans
                    .ToListAsync(); // Converting the result to a list

                foreach (var loan in loans) // For each active loan
                {
                    if (loan.DueDate < DateTime.Now) // If the due date has passed
                    {
                        user.Banned = true; // Ban the user
                        var penalty = new Penalty // Create a new penalty
                        {
                            Active = true,
                            CreationDateLog = DateTime.Now,
                            LoanId = loan.Id,
                            PenaltiedMembeId = user.Id,
                            PenaltyTypeId = 1,
                            State = State.Eklendi,
                            DeleteDateLog = null,
                            UpdateDateLog = null
                        };
                        context.Penalties!.Add(penalty); // Add the penalty
                    }
                }
                await userManager.UpdateAsync(user); // Update the user
                await context.SaveChangesAsync(); // Save changes to the database
            }

            var reservations = await context.Reservations!
                .Include(x => x.Member)
                .ThenInclude(x => x!.ApplicationUser)
                .Where(x => x.Active == true)
                .Where(x => x.State != State.Silindi)
                .Where(x => x.Member!.ApplicationUser!.Id == user.Id)
                .ToListAsync();

            if (reservations.Count != 0)
            {
                foreach (var reservation in reservations)
                {
                    if (reservation.ReservationEnd < DateTime.Now)
                    {
                        reservation.Active = false;
                        context.Reservations!.Update(reservation);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        // Getting the user's reservations asynchronously
        public async Task<List<Reservation>> GetReservations(string userName)
        {
            var reservation = await context.Reservations!.Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .Where(x => x.Member!.ApplicationUser!.UserName == userName)
                .Where(x => x.State != State.Silindi)
                .Where(x => x.Active == true).ToListAsync();

            return reservation;
        }

        // Ending a reservation asynchronously
        public async Task EndReservations(string userName, int reservationId)
        {
            var reservation = await context.Reservations!.Include(x => x.Member).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.Employee).ThenInclude(x => x!.ApplicationUser)
                .Include(x => x.StudyTable)
                .Where(x => x.Member!.ApplicationUser!.UserName == userName)
                .Where(x => x.State != State.Silindi)
                .Where(x => x.Active == true)
                .FirstOrDefaultAsync(x => x.Id == reservationId);

            reservation!.Active = false;
            context.Reservations!.Update(reservation);
            await context.SaveChangesAsync();
        }

        // Saving changes to the database
        public Task<bool> SaveChanges()
        {
            var result = context.SaveChangesAsync().IsCompletedSuccessfully;
            return Task.FromResult(result);
        }
    }
}
