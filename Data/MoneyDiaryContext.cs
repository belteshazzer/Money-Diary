using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Data
{
    public class MoneyDiaryDbContext : IdentityDbContext<User>
    {
        public MoneyDiaryDbContext(DbContextOptions<MoneyDiaryDbContext> options)
            : base(options)
        {
        }
        public DbSet<EmailSettings> EmailSettings { get; set; }
        public DbSet<Income> Incomes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure EmailSettings entity
            builder.Entity<EmailSettings>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            builder.Entity<Income>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                .WithMany(u => u.Incomes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

    }
}