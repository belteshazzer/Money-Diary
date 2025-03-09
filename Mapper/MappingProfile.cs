// filepath: /c:/Users/CBE/Projects/personal/Money-Diary/Mappings/MappingProfile.cs
using AutoMapper;
using MoneyDiary.Models.Dtos;
using MoneyDiary.Models.Entities;

namespace MoneyDiary.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Income, IncomeHistoryDto>().ReverseMap();
            
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryHistoryDto>().ReverseMap();

            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Expense, ExpenseHistoryDto>().ReverseMap();
            
            CreateMap<Budget, BudgetDto>().ReverseMap();
            CreateMap<Budget, BudgetHistoryDto>().ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();

            CreateMap<NotificationType, NotificationTypeDto>().ReverseMap();
            
        }
    }
}