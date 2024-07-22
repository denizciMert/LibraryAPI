using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.DAL.Data
{
    public class AccountData(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) : IAccountBase
    {
        public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<bool> UserSignInAsync(ApplicationUser user, string password)
        {
            var result = signInManager.PasswordSignInAsync(user, password, false, false).Result;
            return result.Succeeded;
        }

        public Task<bool> UserSignOutAsync()
        {
            return Task.FromResult(signInManager.SignOutAsync().IsCompletedSuccessfully);
        }

        public async Task<string> GeneratePasswordResetToken(ApplicationUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateEmailChangeToken(ApplicationUser user, string newEmail)
        {
            return await userManager.GenerateChangeEmailTokenAsync(user,newEmail);
        }

        public async Task<string> GenerateEmailConfirmToken(ApplicationUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> PasswordChange(ApplicationUser user, string newPassword, string token)
        {
            return userManager.ResetPasswordAsync(user, token, newPassword).Result.Succeeded;
        }

        public async Task<bool> EmailChange(ApplicationUser user, string newEmail, string token)
        {
            return userManager.ChangeEmailAsync(user,newEmail, token).Result.Succeeded;
        }

        public async Task<bool> EmailConfirm(ApplicationUser user, string token)
        {
            return userManager.ConfirmEmailAsync(user, token).Result.Succeeded;
        }
    }
}
