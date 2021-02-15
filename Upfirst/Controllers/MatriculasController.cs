using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace Upfirst.Controllers
{
    public class MatriculasController : Controller
    {
        private readonly UpFirstDbContext _context;

        public MatriculasController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: CursosAlunos
        public async Task<IActionResult> Index()
        {
            var upFirstDbContext = _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Include(m => m.Pagamento);

            return View(await upFirstDbContext.ToListAsync());
        }

        // GET: CursosAlunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(c => c.Aluno)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
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
        public async Task<IActionResult> Create([Bind("Id,CursoId,AlunoId,Liberado,Data")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", matricula.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", matricula.CursoId);
            return View(matricula);
        }

        // GET: CursosAlunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", matricula.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", matricula.CursoId);
            return View(matricula);
        }

        // POST: CursosAlunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CursoId,AlunoId,Liberado,Data")] Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursosAlunosExists(matricula.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Id", matricula.AlunoId);
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", matricula.CursoId);
            return View(matricula);
        }

        // GET: CursosAlunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(c => c.Aluno)
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: CursosAlunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursosAlunosExists(int id)
        {
            return _context.Matriculas.Any(e => e.Id == id);
        }


    }
}
