using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socset.Models;

namespace socset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTweets() {
            var tweets = await _context.Tweets.Include(t => t.user).ToListAsync();
            return Ok(tweets);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTweet([FromBody] Tweet tweet)
        {
            if (tweet == null || string.IsNullOrEmpty(tweet.Content))
            return BadRequest("Invalid tweet data");

            tweet.UserId = User.FindFirst("sub")?.Value;
            tweet.CreatedAt = DateTime.Now;

            _context.Tweets.Add(tweet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllTweets), new {id = tweet.Id}, tweet);
        }

        private readonly AppDbContext _context;
        public TweetController(AppDbContext context) { 
        _context = context;
        }

    }
}
