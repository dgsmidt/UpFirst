using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LazZiya.ExpressLocalization;
using LazZiya.ExpressLocalization.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Upfirst.Data;

namespace WebCore.Areas.Identity.Pages.Account.Manage
{
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;

        public SetPasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [ExRequired]
            [ExStringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [ExCompare("NewPassword")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                var msg = _loc.GetLocalizedString("Unable to load user with ID '{0}'.", _userManager.GetUserId(User));
                return NotFound(msg);
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword", new { culture });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string msg;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                msg = _loc.GetLocalizedString("Unable to load user with ID '{0}'.", _userManager.GetUserId(User));
                return NotFound(msg);
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _loc.GetLocalizedString(culture, "Your password has been set.");

            //return RedirectToPage($"~/{culture}");
            return RedirectToPage();
        }
    }
}
