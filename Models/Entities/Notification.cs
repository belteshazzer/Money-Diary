using System;
using System.Collections.Generic;

namespace MoneyDiary.Models.Entities;

public partial class Notification
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public int? NotificationId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual NotificationType? NotificationNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
