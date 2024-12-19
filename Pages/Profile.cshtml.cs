using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace googleLogin.Pages
{
    public class ProfileModel : PageModel
    {
       // public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public async Task OnGetAsync()
        {
          //  var authenticateResult = await HttpContext.AuthenticateAsync();
            //IsAuthenticated = authenticateResult.Succeeded;

            if (User.Identity.IsAuthenticated)
            {
               // var claims = authenticateResult.Principal.Claims;
                Name = User.FindFirstValue(ClaimTypes.Name);
                Email = User.FindFirstValue(ClaimTypes.Email);
            }
        }
    }

}
