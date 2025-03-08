using MoneyDiary.Common;

namespace MoneyDiary.Models.Dtos
{
    public class NotificationTypeDto
    {
        public string Title { get; set; } = null!;

        public string? Message { get; set; }
    }

    public class NotificationTypeHistoryDto : ISoftDeletable
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Message { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }

    }
}