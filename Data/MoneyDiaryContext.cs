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
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Report> Reports { get; set; }


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
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.Currency).HasDefaultValue("ETB");
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.HasOne(e => e.User)
                .WithMany(u => u.Incomes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Expense>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Currency).HasDefaultValue("ETB");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasOne(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Category>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);

                entity.HasMany(e => e.Budgets);
            });

            builder.Entity<Budget>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Currency).HasDefaultValue("ETB");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasOne(e => e.Category)
                .WithMany(c => c.Budgets)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<EmailSettings>(entity => 
            {
                entity.HasKey(e => e.Id);
            });
            builder.Entity<Notification>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasOne(e => e.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.Budget)
                .WithMany(b => b.Notifications)
                .HasForeignKey(e => e.BudgetId);
                entity.HasOne(e => e.NotificationType)
                .WithMany(nt => nt.Notifications)
                .HasForeignKey(e => e.NotificationTypeId);
            });

            builder.Entity<Report>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.HasOne(e => e.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<NotificationType>(entity => 
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
                entity.Property(e => e.Title).IsRequired();
                entity.HasIndex(e => e.Title).IsUnique();
            });
        }

    }
}