namespace MoneyDiary.Common
{
    public interface ISoftDeletable
    {
        bool? IsDeleted { get; set; }
    }
}