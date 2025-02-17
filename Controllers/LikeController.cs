using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socset.Models;
using System.Security.Claims;

namespace socset.Controllers
{
    public class LikeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LikeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("{tweetId}")]
        [Authorize]

        public async Task<IActionResult> LikeTweet(int tweetId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }
            var tweet = await _context.Tweets.FirstOrDefaultAsync(t => t.Id == tweetId);
            if (tweet == null)
            {
                return NotFound("Tweet not found");
            }
            var exisitingLike = await _context.Likes.FirstOrDefaultAsync(f => f.TweetId == tweetId && f.UserId == userId);
            if (exisitingLike != null)
            {
                return BadRequest("You have already liked this tweet");
            }
            var like = new Like { TweetId = tweetId, UserId = userId };
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return Ok("Like added succesfully");


        }
        [HttpDelete("unlike/{tweetId}")]
        [Authorize]
        public async Task<IActionResult> UnlikeTweet(int tweetId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Invalid token");
            }
            var like = await _context.Likes.FirstOrDefaultAsync(l => l.TweetId == tweetId && l.UserId == userId);
            if (like == null)
            {
                return NotFound("You have not liked this tweet");
            }
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return Ok("Like removed succesfully");



        }
    }
}
