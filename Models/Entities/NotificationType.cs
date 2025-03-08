using System;
using System.Collections.Generic;
using MoneyDiary.Common;

namespace MoneyDiary.Models.Entities;

public partial class NotificationType : ISoftDeletable
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Message { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
