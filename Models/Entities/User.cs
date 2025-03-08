
using Microsoft.AspNetCore.Identity;
using MoneyDiary.Common;

namespace MoneyDiary.Models.Entities
{
    public class User : IdentityUser, ISoftDeletable
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsDeleted { get; set; }

        public string? History { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

         public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}