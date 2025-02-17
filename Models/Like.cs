using System.ComponentModel.DataAnnotations.Schema;

namespace socset.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int TweetId {  get; set; }
        [ForeignKey("TweetId")]
        public Tweet Tweet{ get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
    }
}
