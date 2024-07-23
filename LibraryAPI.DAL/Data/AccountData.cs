using LibraryAPI.DAL.Data.Interfaces;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.DAL.Data
{
    public class AccountData(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAccountBase
    {
        public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            
            return user;
        }

        public async Task<bool> UserSignInAsync(ApplicationUser user, string password)
        {
            return signInManager.PasswordSignInAsync(user, password, false, false).Result.Succeeded;
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
            return await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
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
            var result = userManager.ChangeEmailAsync(user, newEmail, token).Result.Succeeded;
            await userManager.UpdateNormalizedEmailAsync(user);
            return result;
        }

        public async Task<bool> EmailConfirm(ApplicationUser user, string token)
        {
            return userManager.ConfirmEmailAsync(user, token).Result.Succeeded;
        }

        public async Task<bool> IsEmailConfirmed(ApplicationUser user)
        {
            return userManager.IsEmailConfirmedAsync(user).Result;
        }

        public async Task<bool> ChangeUserRole(ApplicationUser user)
        {
            var role0 = 0;
            var role1 = 1;
            var role0Name = ((UserRole)role0).ToString();
            var role1Name = ((UserRole)role1).ToString();
            var isUserRole0 = await userManager.IsInRoleAsync(user, role0Name);
            if (!isUserRole0)
            {
                var isUserRole1 = await userManager.IsInRoleAsync(user, role1Name);
                if (!isUserRole1)
                {
                    return false;
                }

                return true;
            }
            var removeRole = userManager.RemoveFromRoleAsync(user, role0Name).Result;
            if (!removeRole.Succeeded)
            {
                return false;
            }
            var addRole = userManager.AddToRoleAsync(user, role1Name).Result;
            if (!addRole.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}