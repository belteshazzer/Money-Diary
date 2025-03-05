using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace MoneyDiary.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettingsService _emailSettingsService;

        public EmailSender(EmailSettingsService emailSettingsService)
        {
            _emailSettingsService = emailSettingsService;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailSettings = await _emailSettingsService.GetEmailSettingsAsync();

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(emailSettings.SenderName, emailSettings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress("", email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.UseSsl);
            await client.AuthenticateAsync(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}