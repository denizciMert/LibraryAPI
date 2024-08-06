using LibraryAPI.Entities.Models; // Importing entity models

namespace LibraryAPI.DAL.Data.Interfaces
{
    // Defining an interface for account-related operations
    public interface IAccountBase
    {
        // Method to find a user by their username asynchronously
        public Task<ApplicationUser> FindUserByUserNameAsync(string userName);

        // Method to sign in a user asynchronously
        public Task<bool> UserSignInAsync(ApplicationUser user, string password);

        // Method to sign out a user asynchronously
        public Task<bool> UserSignOutAsync();

        // Method to generate a password reset token for a user asynchronously
        public Task<string> GeneratePasswordResetToken(ApplicationUser user);

        // Method to generate an email change token for a user asynchronously
        public Task<string> GenerateEmailChangeToken(ApplicationUser user, string newEmail);

        // Method to generate an email confirmation token for a user asynchronously
        public Task<string> GenerateEmailConfirmToken(ApplicationUser user);

        // Method to change a user's password asynchronously using a token
        public Task<bool> PasswordChange(ApplicationUser user, string newPassword, string token);

        // Method to change a user's email asynchronously using a token
        public Task<bool> EmailChange(ApplicationUser user, string newEmail, string token);

        // Method to confirm a user's email asynchronously using a token
        public Task<bool> EmailConfirm(ApplicationUser user, string token);

        // Method to check if a user's email is confirmed asynchronously
        public Task<bool> IsEmailConfirmed(ApplicationUser user);

        // Method to change a user's role asynchronously
        public Task<bool> ChangeUserRole(ApplicationUser user);
    }
}