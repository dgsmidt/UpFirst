using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class PerguntasQuestionarioController : Controller
    {
        private readonly UpFirstDbContext _context;

        public PerguntasQuestionarioController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: PerguntasQuestionario
        public async Task<IActionResult> Index()
        {
            return View(await _context.PerguntasQuestionario.ToListAsync());
        }

        // GET: PerguntasQuestionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaQuestionario = await _context.PerguntasQuestionario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perguntaQuestionario == null)
            {
                return NotFound();
            }

            return View(perguntaQuestionario);
        }

        // GET: PerguntasQuestionario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PerguntasQuestionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] PerguntaQuestionario perguntaQuestionario)
        {
            if (ModelState.IsValid)
            {
                // Só há um questionário
                perguntaQuestionario.QuestionarioId = 1;

                _context.Add(perguntaQuestionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perguntaQuestionario);
        }

        // GET: PerguntasQuestionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaQuestionario = await _context.PerguntasQuestionario
                .Include(q => q.Questionario)
                .Where(q => q.Id == id)
                .SingleOrDefaultAsync();

            if (perguntaQuestionario == null)
            {
                return NotFound();
            }
            return View(perguntaQuestionario);
        }

        // POST: PerguntasQuestionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,QuestionarioId")] PerguntaQuestionario perguntaQuestionario)
        {
            if (id != perguntaQuestionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perguntaQuestionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerguntaQuestionarioExists(perguntaQuestionario.Id))
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
            return View(perguntaQuestionario);
        }

        // GET: PerguntasQuestionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaQuestionario = await _context.PerguntasQuestionario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perguntaQuestionario == null)
            {
                return NotFound();
            }

            return View(perguntaQuestionario);
        }

        // POST: PerguntasQuestionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perguntaQuestionario = await _context.PerguntasQuestionario.FindAsync(id);
            _context.PerguntasQuestionario.Remove(perguntaQuestionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerguntaQuestionarioExists(int id)
        {
            return _context.PerguntasQuestionario.Any(e => e.Id == id);
        }
    }
}
