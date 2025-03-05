using Microsoft.EntityFrameworkCore;
using MoneyDiary.Data;
using MoneyDiary.Models.Entities;
using System;
using System.Threading.Tasks;

namespace MoneyDiary.Services.EmailService
{
    public class EmailSettingsService
    {
        private readonly MoneyDiaryDbContext _context;

        public EmailSettingsService(MoneyDiaryDbContext context)
        {
            _context = context;
        }

        public async Task<EmailSettings> GetEmailSettingsAsync()
        {
            try
            {
                var emailSettings = await _context.EmailSettings.FirstOrDefaultAsync() ?? throw new InvalidOperationException("Email settings not found.");
                return emailSettings;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving email settings.", ex);
            }
        }

        public async Task UpdateEmailSettingsAsync(EmailSettings emailSettings)
        {
            try
            {
                _context.EmailSettings.Update(emailSettings);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating email settings.", ex);
            }
        }

        public async Task RegisterEmailSettingsAsync(EmailSettings emailSettings)
        {
            try
            {
                await _context.EmailSettings.AddAsync(emailSettings);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering email settings.", ex);
            }
        }
    }
}