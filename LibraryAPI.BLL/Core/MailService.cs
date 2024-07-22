using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using Microsoft.Extensions.Configuration;

namespace LibraryAPI.BLL.Core
{
    public class MailService
    {
        private readonly string _mailslurpApiKey;

        public MailService(IConfiguration configuration)
        {
            _mailslurpApiKey = configuration["MailSlurp:ApiKey"];
        }

        public async Task<ServiceResult<bool>> SendEmailAsync(SendEmailOptions emailOptions)
        {
            var configuration = new Configuration();
            configuration.ApiKey.Add("x-api-key", _mailslurpApiKey);
            configuration.Timeout = 120_000;

            var inboxController = new InboxControllerApi(configuration);
            var inbox = inboxController.CreateInboxWithDefaults();
            try
            {
                await inboxController.SendEmailAsync(inbox.Id, emailOptions);
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (ApiException e)
            {
                return ServiceResult<bool>.FailureResult($"E-posta gönderimi sırasında bir hata oluştu: {e.Message}");
            }
        }

        public async Task<ServiceResult<bool>> SendPasswordResetTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail },
                Subject = "Denizci Kütüphanesi Şifre Sıfırlama",
                Body = $"Şifre sıfırlama talebinizi aldık. Bu kodu kullanarak şifrenizi sıfırlayabilirsiniz.\nToken: {token}"
            };

            var result = SendEmailAsync(email).Result;
            if (!result.Success)
            {
                return ServiceResult<bool>.FailureResult(result.ErrorMessage);
            }
            return ServiceResult<bool>.SuccessResult(result.Data);
        }

        public async Task<ServiceResult<bool>> SendEmailChangeTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail },
                Subject = "Denizci Kütüphanesi Mail Değişimi",
                Body = $"Mail değiştirme talebinizi aldık. Bu kodu kullanarak mail adresinizi değiştirebilirsiniz.\nToken: {token}"
            };

            var result = SendEmailAsync(email).Result;
            if (!result.Success)
            {
                return ServiceResult<bool>.FailureResult(result.ErrorMessage);
            }
            return ServiceResult<bool>.SuccessResult(result.Data);
        }

        public async Task<ServiceResult<bool>> SendEmailConfirmTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail },
                Subject = "Denizci Kütüphanesi Mail Doğrulama",
                Body = $"Mail doğrulama talebinizi aldık. Bu kodu kullanarak mail adresinizi doğrulayabilirsiniz.\nToken: {token}"
            };

            var result = SendEmailAsync(email).Result;
            if (!result.Success)
            {
                return ServiceResult<bool>.FailureResult(result.ErrorMessage);
            }
            return ServiceResult<bool>.SuccessResult(result.Data);
        }
    }
}
