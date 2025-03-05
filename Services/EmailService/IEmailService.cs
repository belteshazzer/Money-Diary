using System.Threading.Tasks;

namespace MoneyDiary.Services.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}