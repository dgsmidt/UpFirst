using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LazZiya.ExpressLocalization.DataAnnotations;
using LazZiya.ExpressLocalization;
using Web.Data;

namespace WebCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            ISharedCultureLocalizer loc)
        {
            _signInManager = signInManager;
            _logger = logger;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [ExRequired]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [ExRequired]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string email = "")
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (email != "")
                Input = new InputModel { Email = email };

            returnUrl = returnUrl ?? Url.Content($"~/{culture}");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content($"~/{culture}");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe, culture });
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe, culture });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    //return RedirectToPage("./Lockout", new { culture });
                    return RedirectToPage("./Lockout", new { culture });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, _loc.GetLocalizedString(culture, "Invalid login attempt."));

                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        //public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = await _userManager.FindByEmailAsync(Input.Email);
        //    if (user == null)
        //    {
        //        var msg = _loc.GetLocalizedString(culture, "Verification email sent. Please check your email.");
        //        TempData.Info(msg);
        //    }

        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //    var callbackUrl = Url.Page(
        //        "/Account/ConfirmEmail",
        //        pageHandler: null,
        //        values: new { userId = userId, code = code, culture },
        //        protocol: Request.Scheme);

        //    var mailHeader = _loc.GetLocalizedString(culture, "Confirm your email");
        //    var mailBody = _loc.GetLocalizedString(culture, "Please confirm your account by <a href='{0}'>clicking here</a>.", HtmlEncoder.Default.Encode(callbackUrl));

        //    await _emailSender.SendEmailAsync(
        //        Input.Email,
        //        mailHeader,
        //        mailBody);

        //    TempData.Info("Verification email sent. Please check your email.");
        //    return Page();
        //}
    }
}
