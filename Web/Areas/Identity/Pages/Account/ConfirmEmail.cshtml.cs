using System.Text;
using System.Threading.Tasks;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Web.Data;

namespace WebCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, ISharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index", new { culture });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var msg = _loc.GetLocalizedString(culture, "Unable to load user with ID '{0}'.", userId);
                return NotFound(msg);
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? _loc.GetLocalizedString(culture, "Thank you for confirming your email.") : _loc.GetLocalizedString(culture, "Error confirming your email.");
            return Page();
        }
    }
}
