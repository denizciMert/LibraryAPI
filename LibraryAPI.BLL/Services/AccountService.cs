using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryAPI.BLL.Core;
using LibraryAPI.BLL.Interfaces;
using LibraryAPI.BLL.Mappers;
using LibraryAPI.DAL;
using LibraryAPI.DAL.Data;
using LibraryAPI.Entities.DTOs;
using LibraryAPI.Entities.DTOs.LoanDTO;
using LibraryAPI.Entities.DTOs.PenaltyDTO;
using LibraryAPI.Entities.Enums;
using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.BLL.Services
{
    public class AccountService : ILibraryAccountManager
    {
        private readonly AccountData _accountData;
        private readonly MailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationUserMapper _applicationUserMapper;
        private readonly LoanMapper _loanMapper;
        private readonly PenaltyMapper _penaltyMapper;

        public AccountService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            MailService mailService,
            IConfiguration configuration)
        {
            _accountData = new AccountData(userManager, signInManager, context);
            _mailService = mailService;
            _configuration = configuration;
            _applicationUserMapper = new ApplicationUserMapper();
            _loanMapper = new LoanMapper();
            _penaltyMapper = new PenaltyMapper();
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
                var result = _accountData.PasswordCheck(user, password).Result;
                if (result == false)
                {
                    return ServiceResult<string>.FailureResult("Kullanıcı bilgileriniz hatalı.");
                }
                await _accountData.CheckUserStatus(user);
                var jwt = GenerateJwtToken(user);
                if (user.Banned==true)
                {
                    return ServiceResult<string>.SuccessResult($"Giriş işlemi başarılı ancak hesabınızın engellenmesine sebep olan bir cezanız var. \nJWT Token: {jwt[0]} \nToken Valid To: {jwt[1]}");
                }
                return ServiceResult<string>.SuccessResult($"Başarıyla giriş yapıldı. \nJWT Token: {jwt[0]} \nToken Valid To: {jwt[1]}");

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

        public string[] GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = _accountData.GetClaims(user).Result;
            var roles = _accountData.GetRoles(user).Result;

            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim("UserName",user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new string[] { new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo.ToString()};
        }

        public async Task<ServiceResult<ApplicationUser>> UpdateeProfileAsync(string userName, ApplicationUserPatch patchDto)
        {
            try
            {
                var user = await _accountData.FindUserByUserNameAsync(userName);
                if (user.UserName == null)
                {
                    return ServiceResult<ApplicationUser>.FailureResult("Kullanıcı bulunamadı.");
                }

                _applicationUserMapper.ApplyPatch(user, patchDto);
                var result = await _accountData.UpdateUser(user);

                if (!result.Succeeded)
                {
                    return ServiceResult<ApplicationUser>.FailureResult("Profil güncellenemedi.");
                }

                return ServiceResult<ApplicationUser>.SuccessResult(user);
            }
            catch (Exception ex)
            {
                return ServiceResult<ApplicationUser>.FailureResult($"Bir hata oluştu: {ex.InnerException}");
            }
        }

        public async Task<ServiceResult<List<LoanGet>>> GetUserLoans(ApplicationUser user)
        {
            try
            {
                var result = _accountData.GetLoans(user).Result;
                if (result.Count ==0)
                {
                    return ServiceResult<List<LoanGet>>.FailureResult("Ödünç alınan kitap kaydınız bulunamadı.");
                }
                List<LoanGet> loanGets = new List<LoanGet>();
                foreach (var loan in result)
                {
                    var loanGet = _loanMapper.MapToDto(loan);
                    loanGets.Add(loanGet);
                }
                return ServiceResult<List<LoanGet>>.SuccessResult(loanGets);
            }
            catch (Exception e)
            {
                return ServiceResult<List<LoanGet>>.FailureResult($"Bir hata meydana geldi. Hata: {e.InnerException}");
            }
        }

        public async Task<ServiceResult<List<LoanGet>>> GetReturnedUserLoans(ApplicationUser user)
        {
            try
            {
                var result = _accountData.GetReturnedLoans(user).Result;
                if (result.Count == 0)
                {
                    return ServiceResult<List<LoanGet>>.FailureResult("Ödünç alınan kitap kaydınız bulunamadı.");
                }
                List<LoanGet> loanGets = new List<LoanGet>();
                foreach (var loan in result)
                {
                    var loanGet = _loanMapper.MapToDto(loan);
                    loanGets.Add(loanGet);
                }
                return ServiceResult<List<LoanGet>>.SuccessResult(loanGets);
            }
            catch (Exception e)
            {
                return ServiceResult<List<LoanGet>>.FailureResult($"Bir hata meydana geldi. Hata: {e.InnerException}");
            }
        }

        public async Task<ServiceResult<bool>> ReturnUserLoans(ApplicationUser user, int bookId)
        {
            try
            {
                var result = _accountData.GetLoans(user).Result;
                if (result.Count == 0)
                {
                    return ServiceResult<bool>.FailureResult("Ödünç alınan kitap kaydınız bulunamadı.");
                }

                
                foreach (var loan in result)
                {
                    if (loan.BookId==bookId)
                    {
                        loan.Active = false;
                        await _accountData.ChangeReserveValueForReturnedLoan(bookId, loan.CopyNo);
                        break;
                    }
                }

                var save = _accountData.SaveChanges().Result;
                if (!save)
                {
                    return ServiceResult<bool>.FailureResult("İşlem başarısız oldu.");
                }
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e}");
            }
        }

        public async Task<ServiceResult<List<PenaltyGet>>> GetUserPenalties(ApplicationUser user)
        {
            try
            {
                var result = _accountData.GetPenalties(user).Result;
                if (result.Count == 0)
                {
                    return ServiceResult<List<PenaltyGet>>.FailureResult("Ceza kaydınız bulunamadı.");
                }
                List<PenaltyGet> penaltyGets = new List<PenaltyGet>();
                foreach (var penalty in result)
                {
                    var loanGet = _penaltyMapper.MapToDto(penalty);
                    penaltyGets.Add(loanGet);
                }
                return ServiceResult<List<PenaltyGet>>.SuccessResult(penaltyGets);
            }
            catch (Exception e)
            {
                return ServiceResult<List<PenaltyGet>>.FailureResult($"Bir hata meydana geldi. Hata: {e.InnerException}");
            }
        }

        public async Task<ServiceResult<bool>> PayUserPenalty(ApplicationUser user, int penaltyId, float amount)
        {
            try
            {
                var result = _accountData.GetPenalties(user).Result;
                if (result.Count == 0)
                {
                    return ServiceResult<bool>.FailureResult("Ceza kaydınız bulunamadı.");
                }
                bool isPaid = false;
                foreach (var penalty in result)
                {
                    if (penalty.Id == penaltyId)
                    {
                        if (penalty.PenaltyType.AmountToPay == amount)
                        {
                            penalty.Active = false;
                            isPaid = true;
                            user.Banned = false;
                            penalty.UpdateDateLog= DateTime.Now;
                            penalty.State = State.Eklendi;
                        }
                        else
                        {
                            return ServiceResult<bool>.FailureResult("Ödenen tutar hatalı.");
                        }
                        break;
                    }
                }
                if (!isPaid)
                {
                    return ServiceResult<bool>.FailureResult("Ödeme işlemi başarısız oldu.");
                }

                await _accountData.UpdateUser(user);
                await _accountData.SaveChanges();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.FailureResult($"Bir hata meydana geldi. Hata: {e.InnerException}");
            }
        }

        public async Task<ServiceResult<string>> RateBook(ApplicationUser user, int bookId, float rating)
        {
            try
            {
                var loans = _accountData.GetReturnedLoans(user).Result;
                if (loans.Count == 0)
                {
                    return ServiceResult<string>.FailureResult("Geri getirilen kitap bulunamadı.");
                }

                if (await _accountData.IsRatedBefore(user,bookId,rating))
                {
                    return ServiceResult<string>.SuccessResult("Puanlamanız değiştirildi.");
                }

                foreach (var loan in loans)
                {
                    if (loan.BookId == bookId)
                    {
                        var rate = new BookRating
                        {
                            RatedBookId = bookId,
                            RaterMemberId = user.Id,
                            Rate = rating
                        };
                        await _accountData.AddRatingAndSave(rate);
                        await _accountData.CalculateRating(bookId);
                        return ServiceResult<string>.SuccessResult("Puanlamanız kaydedildi.");
                    }
                }
                return ServiceResult<string>.FailureResult("Bu kitabı geri getirmeden puanlayamazsınız.");
            }
            catch (Exception e)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {e.InnerException}");
            }
        }

        public async Task<ServiceResult<string>> BanUser(ApplicationUser user)
        {
            try
            {
                if (user.UserName == _configuration["Sudo:Username"])
                {
                    return ServiceResult<string>.FailureResult("Yönetici kullanıcı engellenemez.");
                }
                user.Banned = true;
                await _accountData.UpdateUser(user);
                return ServiceResult<string>.SuccessResult($"{user.UserName} kullanıcısı engellendi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {ex.InnerException}");
            }
        }

        public async Task<ServiceResult<string>> UnBanUser(ApplicationUser user)
        {
            try
            {
                user.Banned = false;
                await _accountData.UpdateUser(user);
                return ServiceResult<string>.SuccessResult($"{user.UserName} kullanıcısının engeli kaldırıldı.");
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.FailureResult($"Bir hata meydana geldi. Hata: {ex.InnerException}");
            }
        }
    }
}