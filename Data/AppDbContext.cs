namespace socset.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<Follow> Follows { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Follow>()
            .HasOne(f => f.Follower)
            .WithMany()
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Follow>()
            .HasOne(f => f.Following)
            .WithMany()
            .HasForeignKey(f => f.FollowingId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Like>()
            .HasOne(f => f.Tweet)
            .WithMany()
            .HasForeignKey(f => f.TweetId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Like>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        //builder.Entity<IdentityUserLogin<string>>(
        //    ).HasKey(l => new { l.LoginProvider, l.ProviderKey });
        /* builder.Entity<Tweet>()
             .HasOne(t => t.User)
             .WithMany()
             .HasForeignKey(t => t.UserId);*/
    }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Notification> Notifications { get; set; }

}

