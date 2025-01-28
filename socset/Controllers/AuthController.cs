using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using socset.Models;
namespace socsetControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var result = await _userManager.CreateAsync(model, "DefaultPassword123!");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User register succesfully");
        }
        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok("Login logic here");
        }
    }

}