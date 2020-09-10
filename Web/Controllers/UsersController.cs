using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly UpFirstDbContext _dbContext;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, UpFirstDbContext dbConext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbConext;
        }
        public async Task<IActionResult> Index()
        {
            List<UsersVM> model = new List<UsersVM>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                model.Add(new UsersVM { Email = user.UserName, Administrator = roles.Contains("Administrator") });
            }

            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(List<UsersVM> users)
        {
            //var boleano = collection.TryGetValue("item.Email", out var emails);
            //var boleano1 = collection.TryGetValue("item.Administrator", out var admins);

            foreach (var user in users)
            {
                var usuario = await _userManager.FindByEmailAsync(user.Email);

                if (user.Administrator)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Administrator"));
                    var res = await _userManager.AddToRoleAsync(usuario, "Administrator");
                }
                else
                {
                    var res = await _userManager.RemoveFromRoleAsync(usuario, "Administrator");
                }

            }

            try
            {
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            await _dbContext.ExcluirAlunoAsync(user.Id);

            var result = await _userManager.DeleteAsync(user);
            
            return RedirectToAction("Index");
        }
    }
}
