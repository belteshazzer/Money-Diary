using System;
using System.Collections.Generic;
using MoneyDiary.Common;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Models.Entities;

public partial class Budget : ISoftDeletable
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public int CategoryId { get; set; }

    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string? Period { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
