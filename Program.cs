using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyDiary.Data;
using MoneyDiary.Models.Entities;
using MoneyDiary.Services.EmailService;
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

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();
app.Run();
