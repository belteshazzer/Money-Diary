using System;
using System.Collections.Generic;
using MoneyDiary.Common;

namespace MoneyDiary.Models.Entities;

public partial class Notification : ISoftDeletable
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public int? NotificationId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual NotificationType? NotificationType { get; set; }

    public virtual User User { get; set; } = null!;
}
