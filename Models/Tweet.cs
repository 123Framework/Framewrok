using System.ComponentModel.DataAnnotations.Schema;

namespace socset.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string UserId { get; set; }

       // public User User { get; set; }
        public string Content { get; set; }
        public string Media { get; set; }
        public bool IsRead { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey("UserId")]
        public virtual User user { get; set; } = null;
    }
}
