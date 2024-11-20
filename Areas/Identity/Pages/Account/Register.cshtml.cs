using auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace auth.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _userEmailStore;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _userEmailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

        }
        [BindProperty] public string ReturnUrl { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            [StringLength(50)]
            public string FirstName {  get; set; }
           

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(50)]
            public string LastName {  get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime DateofBirth { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "email")]
            
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "the {0} must be at least {2} and at max {1} characters long", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "password")]

            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "the Password doesnt match")]
            public string ConfirmPassword { get; set; }
        }






        public async Task OnGetAsync(string returnUrl = null)

        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
 
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");


            // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.DateofBirth = Input.DateofBirth;
                user.DateofBirth = Input.DateofBirth;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _userEmailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with pass");

                    //await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


                /*
                                    var userId = await _userManager.GetUserIdAsync(user);
                                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                                    var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "Identity", UserId = userId, Code = code, ReturnUrl = returnUrl },
                                        protocol: Request.Scheme);
                                    await _emailSender.SendEmailAsync(Input.Email, "Confirm email", $"please confirm ur account by <a href='{System.Text.Encodings.Web.HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>");

                                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                                    {
                                        return RedirectToPage("registerConfirmation", new { email = Input.Email, ReturnUrl = returnUrl });
                                    }
                                    else
                                    {
                                        await _signInManager.SignInAsync(user, isPersistent: false);
                                        return LocalRedirect(returnUrl);
                                    }
                                }

                            }
                */
                return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of {nameof(ApplicationUser)}" + $"Ensure that {nameof(ApplicationUser)} is not an abstract class and has a parameterless constructor, or alternatively"
                    + $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default ui requires a user store with email support");

            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
