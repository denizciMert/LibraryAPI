using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.BLL.Core;
using LibraryAPI.Entities.Models;

namespace LibraryAPI.BLL.Interfaces
{
    public interface ILibraryAccountManager
    {
        public Task<ServiceResult<ApplicationUser>> FindUserByUserName(string userName);
        public Task<ServiceResult<string>> Login(ApplicationUser user, string password);
        public Task<ServiceResult<string>> Logout();
        public Task<ServiceResult<string>> ForgetPassword(ApplicationUser user);
        public Task<ServiceResult<string>> ResetPassword(ApplicationUser user, string newPassword, string token);
        public Task<ServiceResult<string>> RequestEmailChange(ApplicationUser user, string newEmail);
        public Task<ServiceResult<string>> RequestEmailConfirm(ApplicationUser user);
        public Task<ServiceResult<string>> ChangeEmail(ApplicationUser user, string newEmail, string token);
        public Task<ServiceResult<string>> ConfirmEmail(ApplicationUser user, string token);
    }
}