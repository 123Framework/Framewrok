using Microsoft.AspNetCore.Identity;

namespace auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
