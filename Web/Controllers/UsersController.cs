using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
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

                model.Add(new UsersVM { Email = user.UserName, Administrator = roles.Contains("Administrator"), Id = user.Id, Nome = user.Nome, EmailConfirmed = user.EmailConfirmed });
            }

            return View(model);
        }
        public ActionResult Edit(string email)
        {
            return View();
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
                    _ = await _userManager.AddToRoleAsync(usuario, "Administrator");
                }
                else
                {
                    _ = await _userManager.RemoveFromRoleAsync(usuario, "Administrator");
                }

                usuario.EmailConfirmed = user.EmailConfirmed;

                await _userManager.UpdateAsync(usuario);
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

            var aluno = await _dbContext.Alunos.Where(a => a.Email == email).SingleOrDefaultAsync();

            if (aluno != null) { 
                await _dbContext.RemoverDadosAlunoAsync(aluno.Id);
                _dbContext.Alunos.Remove(aluno);
                await _dbContext.SaveChangesAsync();
            }

            _ = await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ExcluirMatriculas(string userId)
        {
            Aluno aluno = await _dbContext.Alunos
                .Where(a => a.UserId == userId)
                .SingleOrDefaultAsync();

            if (aluno != null)
                await _dbContext.RemoverDadosAlunoAsync(aluno.Id);

            return RedirectToAction("Index");
        }
    }
}
