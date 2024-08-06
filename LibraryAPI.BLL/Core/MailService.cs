using mailslurp.Api; // Importing the MailSlurp API functionalities
using mailslurp.Client; // Importing the MailSlurp client functionalities
using mailslurp.Model; // Importing the MailSlurp model classes
using Microsoft.Extensions.Configuration; // Importing the configuration functionalities

namespace LibraryAPI.BLL.Core
{
    // Service class for handling email operations
    public class MailService
    {
        private readonly string? _mailslurpApiKey; // Field to store the MailSlurp API key

        // Constructor to initialize the MailService with configuration
        public MailService(IConfiguration configuration)
        {
            _mailslurpApiKey = configuration["MailSlurp:ApiKey"]; // Retrieving the API key from configuration
        }

        // Method to send an email asynchronously using MailSlurp
        public async Task<ServiceResult<bool>> SendEmailAsync(SendEmailOptions emailOptions)
        {
            var configuration = new Configuration(); // Creating a new MailSlurp configuration instance
            configuration.ApiKey.Add("x-api-key", _mailslurpApiKey); // Adding the API key to the configuration
            configuration.Timeout = 120_000; // Setting the timeout to 120 seconds

            var inboxController = new InboxControllerApi(configuration); // Creating a new InboxControllerApi instance
            var inbox = await inboxController.CreateInboxWithDefaultsAsync(); // Creating a new inbox with default settings
            try
            {
                await inboxController.SendEmailAsync(inbox.Id, emailOptions); // Sending the email using the created inbox
                return ServiceResult<bool>.SuccessResult(true); // Returning success result
            }
            catch (ApiException e)
            {
                return ServiceResult<bool>.FailureResult($"E-posta gönderimi sırasında bir hata oluştu: {e.Message}"); // Returning failure result with error message
            }
        }

        // Method to send a password reset token email
        public Task<ServiceResult<bool>> SendPasswordResetTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail }, // Setting the recipient email address
                Subject = "Denizci Kütüphanesi Şifre Sıfırlama", // Setting the email subject
                Body = $"Şifre sıfırlama talebinizi aldık. Bu kodu kullanarak şifrenizi sıfırlayabilirsiniz.\nToken: {token}" // Setting the email body
            };

            var result = SendEmailAsync(email).Result; // Sending the email synchronously
            if (!result.Success)
            {
                return Task.FromResult(ServiceResult<bool>.FailureResult(result.ErrorMessage)); // Returning failure result if email sending fails
            }
            return Task.FromResult(ServiceResult<bool>.SuccessResult(result.Data)); // Returning success result
        }

        // Method to send an email change token email
        public Task<ServiceResult<bool>> SendEmailChangeTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail }, // Setting the recipient email address
                Subject = "Denizci Kütüphanesi Mail Değişimi", // Setting the email subject
                Body = $"Mail değiştirme talebinizi aldık. Bu kodu kullanarak mail adresinizi değiştirebilirsiniz.\nToken: {token}" // Setting the email body
            };

            var result = SendEmailAsync(email).Result; // Sending the email synchronously
            if (!result.Success)
            {
                return Task.FromResult(ServiceResult<bool>.FailureResult(result.ErrorMessage)); // Returning failure result if email sending fails
            }
            return Task.FromResult(ServiceResult<bool>.SuccessResult(result.Data)); // Returning success result
        }

        // Method to send an email confirmation token email
        public Task<ServiceResult<bool>> SendEmailConfirmTokenMail(string toEmail, string token)
        {
            var email = new SendEmailOptions
            {
                To = new List<string> { toEmail }, // Setting the recipient email address
                Subject = "Denizci Kütüphanesi Mail Doğrulama", // Setting the email subject
                Body = $"Mail doğrulama talebinizi aldık. Bu kodu kullanarak mail adresinizi doğrulayabilirsiniz.\nToken: {token}" // Setting the email body
            };

            var result = SendEmailAsync(email).Result; // Sending the email synchronously
            return Task.FromResult(!result.Success ? ServiceResult<bool>.FailureResult(result.ErrorMessage) : ServiceResult<bool>.SuccessResult(result.Data)); // Returning the result based on email sending success or failure
        }
    }
}
