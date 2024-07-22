using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.BLL.Core
{
    public class MailService
    {
        private readonly string _mailslurpApiKey;

        public MailService(IConfiguration configuration)
        {
            _mailslurpApiKey = configuration["MailSlurp:ApiKey"];
        }

        public async Task<ServiceResult<bool>> SendPasswordResetEmailAsync(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail },
                Subject = "Denizci Kütüphanesi Şifre Sıfırlama",
                Body = $"Şifre sıfırlama talebinizi aldık. Bu kodu kullanarak şifrenizi sıfırlayabilirsiniz. \n Token: {token}"
            };

            var configuration = new Configuration();
            configuration.ApiKey.Add("x-api-key", _mailslurpApiKey);
            configuration.Timeout = 120_000;

            var inboxController = new InboxControllerApi(configuration);
            var inbox = inboxController.CreateInboxWithDefaults();
            try
            {
                await inboxController.SendEmailAsync(inbox.Id, email);
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (ApiException e)
            {
                return ServiceResult<bool>.FailureResult($"E-posta gönderimi sırasında bir hata oluştu: {e.Message}");
            }
        }
    }
}
