using LibraryAPI.BLL.Core; // Importing the core functionalities of the BLL
using LibraryAPI.Entities.DTOs; // Importing the general DTOs
using LibraryAPI.Entities.DTOs.LoanDTO; // Importing the DTOs for Loan
using LibraryAPI.Entities.DTOs.PenaltyDTO; // Importing the DTOs for Penalty
using LibraryAPI.Entities.Models; // Importing the entity models

namespace LibraryAPI.BLL.Interfaces
{
    // Interface for library account management
    public interface ILibraryAccountManager
    {
        // Method to find a user by their username
        public Task<ServiceResult<ApplicationUser>> FindUserByUserName(string userName);

        // Method to find a user by their ID
        public Task<ServiceResult<ApplicationUser>> FindUserById(string id);

        // Method to login a user
        public Task<ServiceResult<string>> Login(ApplicationUser user, string password);

        // Method to logout a user
        public Task<ServiceResult<string>> Logout();

        // Method to handle forgotten password process
        public Task<ServiceResult<string>> ForgetPassword(ApplicationUser user);

        // Method to reset a user's password
        public Task<ServiceResult<string>> ResetPassword(ApplicationUser user, string newPassword, string token);

        // Method to request an email change
        public Task<ServiceResult<string>> RequestEmailChange(ApplicationUser user, string newEmail);

        // Method to request email confirmation
        public Task<ServiceResult<string>> RequestEmailConfirm(ApplicationUser user);

        // Method to change a user's email
        public Task<ServiceResult<string>> ChangeEmail(ApplicationUser user, string newEmail, string token);

        // Method to confirm a user's email
        public Task<ServiceResult<string>> ConfirmEmail(ApplicationUser user, string token);

        // Method to update a user's profile asynchronously
        public Task<ServiceResult<ApplicationUser>> UpdateProfileAsync(string userName, ApplicationUserPatch patchDto);

        // Method to get a user's loans
        public Task<ServiceResult<List<LoanGet>>> GetUserLoans(ApplicationUser user);

        // Method to get a user's returned loans
        public Task<ServiceResult<List<LoanGet>>> GetReturnedUserLoans(ApplicationUser user);

        // Method to return a user's loans
        public Task<ServiceResult<bool>> ReturnUserLoans(ApplicationUser user, int bookId);

        // Method to get a user's penalties
        public Task<ServiceResult<List<PenaltyGet>>> GetUserPenalties(ApplicationUser user);

        // Method to pay a user's penalty
        public Task<ServiceResult<bool>> PayUserPenalty(ApplicationUser user, int penaltyId, float amount);

        // Method to rate a book
        public Task<ServiceResult<string>> RateBook(ApplicationUser user, int bookId, float rating);

        // Method to ban a user
        public Task<ServiceResult<string>> BanUser(ApplicationUser user);

        // Method to unban a user
        public Task<ServiceResult<string>> UnBanUser(ApplicationUser user);

        // Method to get a user's reservations
        public Task<ServiceResult<List<Reservation>>> GetReservations(ApplicationUser user);

        // Method to end a user's reservation
        public Task<ServiceResult<string>> EndReservations(ApplicationUser user, int reservationId);
    }
}
