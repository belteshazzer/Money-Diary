using MoneyDiary.Common;

namespace MoneyDiary.Models.Dtos
{
    public class BudgetDto
    {
        public int CategoryId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string? Period { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }

    public class BudgetHistoryDto : ISoftDeletable
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
    }
}