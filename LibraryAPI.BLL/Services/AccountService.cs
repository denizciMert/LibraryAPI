using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.BLL.Services
{
    public class AccountService : ILibraryAccountManager
    {
        private readonly AccountData _accountData;
        private readonly MailService _mailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MailService mailService)
        {
            _accountData = new AccountData(userManager, signInManager);
            _mailService = mailService;
        }

        public async Task<ServiceResult<ApplicationUser>> FindUserByUserName(string userName)
        {
            try
            {
                ApplicationUser nullUser = new ApplicationUser();
                nullUser = null;
                var user = await _accountData.FindUserByUserNameAsync(userName);
                if (user == nullUser )
                {
                    return ServiceResult<ApplicationUser>.FailureResult("Kullanıcı bilgileriniz hatalı.");
                }
                return ServiceResult<ApplicationUser>.SuccessResult(user);
            }
            catch (Exception e)
            {
                return ServiceResult<ApplicationUser>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> Login(ApplicationUser user, string password)
        {
            try
            {
                var result = _accountData.UserSignInAsync(user, password).Result;
                if (result == false)
                {
                    return ServiceResult<string>.FailureResult("Kullanıcı bilgileriniz hatalı.");
                }
                return ServiceResult<string>.SuccessResult("Başarıyla giriş yapıldı.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> Logout()
        {
            try
            {
                var result = _accountData.UserSignOutAsync().Result;
                if (result == false)
                {
                    return ServiceResult<string>.FailureResult("Çıkış yapılamıyor. Bir süre bekleyin ve tekrar deneyin.");
                }
                return ServiceResult<string>.SuccessResult("Başarıyla çıkış yapıldı.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> ForgetPassword(ApplicationUser user)
        {
            try
            {
                var result = await _accountData.GeneratePasswordResetToken(user);
                if (result is null or "")
                {
                    return ServiceResult<string>.FailureResult("Token oluşturulamıyor. Bir süre bekleyin ve tekrar deneyin.");
                }
                var mail= await _mailService.SendPasswordResetTokenMail(user.Email, result);
                if (!mail.Success)
                {
                    return ServiceResult<string>.FailureResult("Oluşturulan token mail adresinize gönderilemedi.");
                }
                return ServiceResult<string>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> ResetPassword(ApplicationUser user, string newPassword, string token)
        {
            try
            {
                var result = await _accountData.PasswordChange(user, newPassword, token);
                if (!result)
                {
                    return ServiceResult<string>.SuccessResult("Şifreniz güncellenemedi.");
                }
                return ServiceResult<string>.SuccessResult("Şifreniz başarıyla güncellendi.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> RequestEmailChange(ApplicationUser user, string newEmail)
        {
            try
            {
                var result = await _accountData.GenerateEmailChangeToken(user, newEmail);
                if (result is null or "")
                {
                    return ServiceResult<string>.FailureResult("Token oluşturulamıyor. Bir süre bekleyin ve tekrar deneyin.");
                }
                var mail =await _mailService.SendEmailChangeTokenMail(newEmail, result);
                if (!mail.Success)
                {
                    return ServiceResult<string>.FailureResult("Oluşturulan token mail adresinize gönderilemedi.");
                }
                return ServiceResult<string>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> RequestEmailConfirm(ApplicationUser user)
        {
            try
            {
                var result = await _accountData.GenerateEmailConfirmToken(user);
                if (result is null or "")
                {
                    return ServiceResult<string>.FailureResult("Token oluşturulamıyor. Bir süre bekleyin ve tekrar deneyin.");
                }
                var mail = await _mailService.SendEmailConfirmTokenMail(user.Email, result);
                if (!mail.Success)
                {
                    return ServiceResult<string>.FailureResult("Oluşturulan token mail adresinize gönderilemedi.");
                }
                return ServiceResult<string>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> ChangeEmail(ApplicationUser user, string newEmail, string token)
        {
            try
            {
                var result = _accountData.EmailChange(user, newEmail, token).Result;
                if (!result)
                {
                    return ServiceResult<string>.FailureResult("Mail adresiniz değiştirilemedi. Bir süre bekleyin ve tekrar deneyin.");
                }

                return ServiceResult<string>.SuccessResult("Mail adresiniz başarıyla değiştirildi.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<string>> ConfirmEmail(ApplicationUser user, string token)
        {
            try
            {
                var isConfirmed = _accountData.IsEmailConfirmed(user).Result;
                if (isConfirmed)
                {
                    return ServiceResult<string>.SuccessResult("Mail adresiniz zaten doğrulanmış.");
                }
                var result = _accountData.EmailConfirm(user, token).Result;
                if (!result)
                {
                    return ServiceResult<string>.FailureResult("Mail adresiniz doğrulanamadı. Bir süre bekleyin ve tekrar deneyin.");
                }
                var roleResult = _accountData.ChangeUserRole(user).Result;
                if (!roleResult)
                {
                    return ServiceResult<string>.FailureResult("Rol atamaları sırasında hata meydana geldi.");
                }
                return ServiceResult<string>.SuccessResult("Mail adresiniz başarıyla doğrulandı.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }
    }
}