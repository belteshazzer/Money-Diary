using System;
using System.Collections.Generic;

namespace MoneyDiary.Models.Entities;

public partial class EmailSettings
{
    public int Id { get; set; }

    public string SenderName { get; set; } = null!;

    public string SenderEmail { get; set; } = null!;

    public string SmtpServer { get; set; } = null!;

    public int SmtpPort { get; set; }

    public bool UseSsl { get; set; }

    public string SmtpUsername { get; set; } = null!;

    public string SmtpPassword { get; set; } = null!;
}
