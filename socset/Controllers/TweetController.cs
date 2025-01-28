using Microsoft.AspNetCore.Mvc;

namespace socset.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTweets() {
            return Ok("Retrieve all tweets here");
        }
        [HttpPost]
        public IActionResult PostTweet()
        {
            return Ok("Post a new tweet");
        }
    }
}
