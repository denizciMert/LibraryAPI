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
        public Task<ServiceResult<bool>> Login(ApplicationUser user, string password);
        public Task<ServiceResult<bool>> Logout();
        public Task<ServiceResult<bool>> ForgetPassword(ApplicationUser user);
        public Task<ServiceResult<bool>> ResetPassword(ApplicationUser user, string newPassword, string token);
        public Task<ServiceResult<bool>> RequestEmailChange(ApplicationUser user, string newEmail);
        public Task<ServiceResult<bool>> RequestEmailConfirm();
        public Task<ServiceResult<bool>> ChangeEmail();
        public Task<ServiceResult<bool>> ConfirmEmail();
    }
}
