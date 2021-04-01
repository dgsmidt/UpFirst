using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Upfirst.Data;

namespace WebCore.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UpFirstDbContext _dbContext;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UpFirstDbContext dbContext,
            ISharedCultureLocalizer loc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loc = loc;
            _dbContext = dbContext;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        [Display(Name = "User name")]
        public string Username { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public string NomeUsuario { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            // Estes podem ser editados
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Name")]
            public string Nome { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            NomeUsuario = user.Nome;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nome = user.Nome
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                var msg = _loc.GetLocalizedString(culture, "Unable to load user with ID '{0}'.", _userManager.GetUserId(User));
                return NotFound(msg);
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string msg;

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                msg = _loc.GetLocalizedString(culture, "Unable to load user with ID '{0}'.", _userManager.GetUserId(User));
                return NotFound(msg);
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = _loc.GetLocalizedString(culture, "Unexpected error when trying to set phone number.");
                    return RedirectToPage();
                }
            }

            if (Input.Nome != user.Nome)
            {
                user.Nome = Input.Nome;

                Aluno a = _dbContext.Alunos.Where(a => a.UserId == user.Id).SingleOrDefault();

                if (a != null)
                {
                    a.Nome = user.Nome;
                    await _dbContext.SaveChangesAsync();
                }

                var claims = await _userManager.GetClaimsAsync(user);
                var result = await _userManager.RemoveClaimsAsync(user, claims);

                if (!result.Succeeded)
                {
                    StatusMessage = _loc.GetLocalizedString(culture, "Cannot remove user existing claims.");
                    return RedirectToPage();
                }

                result = await _userManager.AddClaimAsync(user, new Claim("Nome", user.Nome));

                if (!result.Succeeded)
                {
                    StatusMessage = _loc.GetLocalizedString(culture, "Cannot add claim to user.");
                    return RedirectToPage();
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _loc.GetLocalizedString(culture, "Your profile has been updated");
            return RedirectToPage();
            //return RedirectToPage($"~/{culture}");
        }
    }
}
