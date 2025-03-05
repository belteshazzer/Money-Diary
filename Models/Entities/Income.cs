using System;
using System.Collections.Generic;

namespace MoneyDiary.Models.Entities;

public partial class Income
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public decimal Amount { get; set; }

    public string? IncomeSource { get; set; }

    public string? Description { get; set; }

    public DateTime? TransactionDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual User User { get; set; } = null!;
}
