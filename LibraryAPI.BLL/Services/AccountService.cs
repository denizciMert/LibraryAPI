using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.Models;
using Newtonsoft.Json.Linq;

namespace LibraryAPI.BLL.Services
{
    public class AccountService : ILibraryAccountManager
    {
        private readonly AccountData _accountData;

        public AccountService(AccountData accountData)
        {
            _accountData = accountData;
        }

        public async Task<ServiceResult<bool>> Login(ApplicationUser user, string password)
        {
            try
            {
                if (await _accountData.FindUserByUserNameAsync(user.UserName) == null)
                {
                    return ServiceResult<bool>.FailureResult("Kullanıcı bulunamadı.");
                }
                await _accountData.UserSignInAsync(user, password);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> Logout()
        {
            try
            {
                await _accountData.UserSignOutAsync();
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> ForgetPassword(ApplicationUser user)
        {
            try
            {
                await _accountData.GeneratePasswordResetToken(user);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> ResetPassword(ApplicationUser user, string newPassword, string token)
        {
            try
            {
                await _accountData.PasswordChange(user,newPassword, token);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> RequestEmailChange(ApplicationUser user, string newEmail)
        {
            try
            {
                await _accountData.GenerateEmailChangeToken(user, newEmail);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> RequestEmailConfirm()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> ChangeEmail()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> ConfirmEmail()
        {
            throw new NotImplementedException();
        }
    }
}
/*
 * try
            {
                var addresses = await _addressData.SelectAllFiltered();
                if (addresses == null || addresses.Count == 0)
                {
                    return ServiceResult<IEnumerable<AddressGet>>.FailureResult("Adres verisi bulunmuyor.");
                }
                List<AddressGet> addressGets = new List<AddressGet>();
                foreach (var address in addresses)
                {
                    var addressGet = _addressMapper.MapToDto(address);
                    addressGets.Add(addressGet);
                }
                return ServiceResult<IEnumerable<AddressGet>>.SuccessResult(addressGets);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<AddressGet>>.FailureResult($"Bir hata oluştu: {ex.Message}");
            }*/