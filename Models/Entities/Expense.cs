using System;
using System.Collections.Generic;

namespace MoneyDiary.Models.Entities;

public partial class Expense
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public int? CategoryId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User User { get; set; } = null!;
}
