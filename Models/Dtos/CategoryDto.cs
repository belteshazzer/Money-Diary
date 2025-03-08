using MoneyDiary.Common;

namespace MoneyDiary.Models.Dtos
{
    public class CategoryDto
    {
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
    }

    public class CategoryHistoryDto : ISoftDeletable
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }
    }
}