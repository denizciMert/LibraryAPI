using LibraryAPI.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;

namespace LibraryAPI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var applicationUser = _userManager.FindByNameAsync(userName).Result;

            if (applicationUser != null)
            {
                var signInResult = _signInManager.PasswordSignInAsync(applicationUser, password, false, false).Result;
                if (signInResult.Succeeded == true)
                {
                    return Ok("Başarıyle giriş yapıldı.");
                }
            }
            return Unauthorized();
        }

        [HttpGet("Logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Hesabınızdan başarıyla çıkış yaptınız.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Çıkış işlemi esnasında bir hata meydana geldi. Hata:", details = ex.Message });
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<string>> ForgetPassword(string userName)
        {
            var applicationUser = await _userManager.FindByNameAsync(userName);
            if (applicationUser == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

            var email = new SendEmailOptions
            {
                To = new List<string> { applicationUser.Email },
                Subject = "Denizci Kütüphanesi Şifre Sıfırlama",
                Body = $"Şifre sıfırlama talebinizi aldık. Bu kodu kullanarak şifrenizi sıfırlayabilirsiniz. \n Token: {token}"
            };

            try
            {
                var configuration = new Configuration();
                configuration.ApiKey.Add("x-api-key", "4a7785812f96534025ddbe3b4d5c5f266f323a43b15c13b478ec8d9732a59f1c");
                configuration.Timeout = 120_000;

                var inboxController = new InboxControllerApi(configuration);
                var inbox = inboxController.CreateInboxWithDefaults();
                await inboxController.SendEmailAsync(inbox.Id, email);

                return Ok(token);
            }
            catch (ApiException e)
            {
                return StatusCode(500, $"E-posta gönderimi sırasında bir hata oluştu: {e.Message}");
            }
        }




        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(string userName, string token, string newPassword)
        {

            ApplicationUser applicationUser = _userManager.FindByNameAsync(userName).Result;

            _userManager.ResetPasswordAsync(applicationUser, token, newPassword).Wait();

            return Ok();
        }
    }
}
