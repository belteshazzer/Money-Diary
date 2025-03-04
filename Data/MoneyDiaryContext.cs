using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoneyDiary.Data
{
    public class MoneyDiaryDbContext : IdentityDbContext
    {
        public MoneyDiaryDbContext(DbContextOptions<MoneyDiaryDbContext> options)
            : base(options)
        {
        }
    }
}