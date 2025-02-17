using System.ComponentModel.DataAnnotations.Schema;

namespace socset.Models
{
    public class Follow
    {
        public int Id { get; set; }

        public string FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        public User Follower { get; set; }
        public string FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        public User Following { get; set; }

    }
}
