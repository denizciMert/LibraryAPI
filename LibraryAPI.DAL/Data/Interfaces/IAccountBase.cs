using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.DAL.Data.Interfaces
{
    public interface IAccountBase
    {
        public Task<ApplicationUser> FindUserByUserNameAsync(string userName);
        public Task<bool> UserSignInAsync(ApplicationUser user, string password);
        public Task<bool> UserSignOutAsync();
        public Task<string> GeneratePasswordResetToken(ApplicationUser user);
        public Task<string> GenerateEmailChangeToken(ApplicationUser user, string newEmail);
        public Task<string> GenerateEmailConfirmToken(ApplicationUser user);
        public Task<bool> PasswordChange(ApplicationUser user, string newPassword, string token);
        public Task<bool> EmailChange(ApplicationUser user, string newEmail, string token);
        public Task<bool> EmailConfirm(ApplicationUser user, string token);
        public Task<bool> ChangeUserRole(ApplicationUser user);
    }
}
