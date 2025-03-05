using System;
using System.Collections.Generic;
using BudgetTracker.Models.Entities;

namespace MoneyDiary.Models.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
