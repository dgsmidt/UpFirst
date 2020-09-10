using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using LazZiya.ExpressLocalization;
using System.Collections;
using System.Collections.Generic;

namespace Web.Controllers
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class AulasController : Controller
    {
        private readonly UpFirstDbContext _context;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;
        public AulasController(UpFirstDbContext context, ISharedCultureLocalizer loc)
        {
            _context = context;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        // GET: Aulas
        public async Task<IActionResult> Index()
        {
            var aulas = await _context.Aulas.Include(a => a.Modulo).ThenInclude(m => m.Curso).ToListAsync();
            return View(aulas);
        }

        // GET: Aulas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas
                .Include(a => a.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // GET: Aulas/Create
        public IActionResult Create(int? cursoId)
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");

            if (cursoId != null)
            {
                ViewData["ModuloId"] = new SelectList(_context.Modulos.Where(m => m.CursoId == cursoId).ToList(), "Id", "Descricao");
            }
            else
            {
                ViewData["ModuloId"] = new SelectList(new List<Modulo>(), "Id", "Descricao");
            }


            return View();
        }
        public JsonResult GetModulosByCursoId(int id)
        {
            var list = _context.Modulos.Where(m => m.CursoId == id).ToList();
            list.Insert(0, new Modulo { Id = 0, Descricao = _loc.GetLocalizedString(culture, "-- Select Module --") });
            return Json(new SelectList(list, "Id", "Descricao"));
        }
        // POST: Aulas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuloId,NumeroAula,Descricao,Video")] Aula aula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
            ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao", aula.ModuloId);
            return View(aula);
        }

        // GET: Aulas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }
            ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao", aula.ModuloId);
            return View(aula);
        }

        // POST: Aulas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuloId,NumeroAula,Descricao,Video")] Aula aula)
        {
            if (id != aula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AulaExists(aula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Id", aula.ModuloId);
            return View(aula);
        }

        // GET: Aulas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas
                .Include(a => a.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // POST: Aulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AulaExists(int id)
        {
            return _context.Aulas.Any(e => e.Id == id);
        }
    }
}
