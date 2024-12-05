using Microsoft.AspNetCore.Identity;
namespace login.Models
{
    public class Users : IdentityUser
    {
        public string FullName {  get; set; }
    }
}
