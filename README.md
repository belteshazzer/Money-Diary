  *****MoneyDiary Web API*****

**Project Overview**

MoneyDiary is a web API designed to help users manage their personal finances. It provides functionalities to track incomes, expenses, budgets, notifications, and categories. The API is built using ASP.NET Core and follows a clean architecture to ensure scalability and maintainability.

****Features****

* *User Management*: Register, login, and manage user profiles.

* *Income Tracking*: Add, update, delete, and view income entries.

* *Expense Tracking*: Add, update, delete, and view expense entries.

* *Budget Management*: Create, update, delete, and view budgets.

* *Notifications*: Send notifications based on financial activities.

* *Categories*: Manage categories for expenses.

****Entities****

* *User* - Represents a user of the application.

* *income* - Represents an income entry for a user.

* *Expense* - Represents an expense entry for a user.

* *Budget* - Represents a budget set by a user.

* *Notification* - Represents a notification sent to a user.

* *NotificationType* - Represents the type of notification.

* *Category* - Represents a category for expenses.


****Technology Stack****

* Backend: ASP.NET Core

* Database: SQL Server

* ORM: Entity Framework Core

* Authentication: ASP.NET Core Identity

* Real-time Communication: SignalR

* Logging: Serilog

* Dependency Injection: Built-in ASP.NET Core DI

* Mapping: AutoMapper


****Prerequisites****

To clone and run this project, you will need:
* .NET 8 SDK
* SQL Server
* Visual Studio 2022 or Visual Studio Code

****Getting Started****

Clone the Repository
````
  git clone https://github.com/yourusername/MoneyDiary.git
  cd MoneyDiary
````

Set Up the Database
1. Update the connection string in appsettings.json to point to your SQL Server instance.
```
   "ConnectionStrings": {
      "DbConnection": "Server=your_server;Database=MoneyDiaryDb;User Id=your_user;Password=your_password;"
    }
```
2. Apply migrations to create the database schema.
```
  dotnet ef migrations add initialMigration
  dotnet ef database update
```
Run the Application
1. Build and run the application.
```
  dotnet build
  dotnet run
```
2. The API will be available at `` https://localhost:5001 `` or ``http://localhost:5000``.

**API Documentation**

Swagger UI is available at ``https://localhost:5001/swagger`` or ``http://localhost:5000/swagger``.

**Additional Information**

*Logging*: The application uses Serilog for logging. Configure the logging settings in appsettings.json.

*Real-time Notifications*: SignalR is used for real-time notifications. The SignalR hub is available at /notificationHub.

*Authentication*: The application uses ASP.NET Core Identity for authentication. Ensure you have configured the ``Email Settings`` first for account confirmation and password recovery.


**Contributing**

Contributions are welcome! Please fork the repository and create a pull request with your changes.
