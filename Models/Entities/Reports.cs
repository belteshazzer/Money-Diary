using System;
using System.Collections.Generic;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Models.Entities;

public partial class Report
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string? ReportType { get; set; }

    public DateOnly ReportDate { get; set; }

    public string? ReportData { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? History { get; set; }

    public virtual User User { get; set; } = null!;
}
