using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace googleLogin.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync()
        {
            await HttpContext.ChallengeAsync("Google");
        }
    }
}
