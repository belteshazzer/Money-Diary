using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyDiary.Data;
using MoneyDiary.Mapper;
using MoneyDiary.Middleware;
using MoneyDiary.Models.Entities;
using MoneyDiary.Repositories;
using MoneyDiary.Services.BudgetService;
using MoneyDiary.Services.CategoryService;
using MoneyDiary.Services.EmailService;
using MoneyDiary.Services.ExpenseService;
using MoneyDiary.Services.IncomeService;
using Org.BouncyCastle.Asn1.X509.Qualified;
// using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddDbContext<MoneyDiaryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MoneyDiaryDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<EmailSettingsService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
