using MoneyDiary.Common;

namespace MoneyDiary.Models.Dtos
{
    public class ExpenseDto
    {
        public int? CategoryId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }

    public class ExpenseHistoryDto : ISoftDeletable
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}