

using Microsoft.AspNetCore.Identity;

namespace MoneyDiary.Models.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}