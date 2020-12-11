using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace Web.Controllers
{
    public class CursosAlunosController : Controller
    {
        private readonly UpFirstDbContext _context;

        public CursosAlunosController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: CursosAlunos
        public async Task<IActionResult> Index()
        {
            var upFirstDbContext = _context.CursosAlunos.Include(c => c.Aluno).Include(c => c.Curso);
            return View(await upFirstDbContext.ToListAsync());
        }

        // GET: CursosAlunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursosAlunos = await _context.CursosAlunos
                .Include(c => c.Aluno)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursosAlunos == null)
            {
                return NotFound();
            }

            return View(cursosAlunos);
        }

        // GET: CursosAlunos/Create
        public IActionResult Create()
        {
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id");
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id");
            return View();
        }

        // POST: CursosAlunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CursoId,AlunoId,Liberado,Data")] CursoAluno cursosAlunos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursosAlunos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", cursosAlunos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", cursosAlunos.CursoId);
            return View(cursosAlunos);
        }

        // GET: CursosAlunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursosAlunos = await _context.CursosAlunos.FindAsync(id);
            if (cursosAlunos == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", cursosAlunos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", cursosAlunos.CursoId);
            return View(cursosAlunos);
        }

        // POST: CursosAlunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CursoId,AlunoId,Liberado,Data")] CursoAluno cursosAlunos)
        {
            if (id != cursosAlunos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursosAlunos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosAlunosExists(cursosAlunos.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", cursosAlunos.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", cursosAlunos.CursoId);
            return View(cursosAlunos);
        }

        // GET: CursosAlunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursosAlunos = await _context.CursosAlunos
                .Include(c => c.Aluno)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursosAlunos == null)
            {
                return NotFound();
            }

            return View(cursosAlunos);
        }

        // POST: CursosAlunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cursosAlunos = await _context.CursosAlunos.FindAsync(id);
            _context.CursosAlunos.Remove(cursosAlunos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosAlunosExists(int id)
        {
            return _context.CursosAlunos.Any(e => e.Id == id);
        }
    }
}
