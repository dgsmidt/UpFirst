﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using LazZiya.ExpressLocalization;
using LazZiya.ExpressLocalization.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Upfirst.Data;

namespace WebCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager, ISharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [ExRequired]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [ExRequired]
            [ExStringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [ExCompare("Password")]
            public string ConfirmPassword { get; set; }
            [Display(Name = "Code")]
            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null, string email = "")
        {
            if (code == null)
            {
                var msg = _loc.GetLocalizedString("A code must be supplied for password reset.");
                return BadRequest(msg);
            }
            else
            {
                Input = new InputModel
                {
                    Email = email,
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation", new { culture });
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation", new { culture, Input.Email });
            }

            foreach (var error in result.Errors)
            {
                // Model binding errors already localized by ExpressLocalization
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
