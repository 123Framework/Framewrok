using Microsoft.AspNetCore.Identity;

namespace socset.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public ICollection<Tweet> Tweets { get; set; } = new List<Tweet>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();


    }
}
