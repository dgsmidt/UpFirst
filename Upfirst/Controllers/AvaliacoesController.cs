using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Upfirst.ViewModels;

namespace Upfirst.Controllers
{
    public class AvaliacoesController : Controller
    {
        private readonly UpFirstDbContext _context;

        public AvaliacoesController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index(int? id, int? perguntaId)
        {
            var viewModel = new AvaliacoesVM
            {
                Avaliacoes = await _context.Avaliacoes
                .Include(a => a.Perguntas)
                 .ThenInclude(p => p.Respostas)
                 .AsNoTracking()
                .ToListAsync()
            };

            if (perguntaId != null)
            {
                viewModel.PerguntaId = perguntaId.Value;
                PerguntaAvaliacao perguntaAvaliacao = await _context.PerguntasAvaliacao.SingleOrDefaultAsync(pa => pa.Id == perguntaId.Value);
                viewModel.RespostasAvaliacao = await _context.RespostasAvaliacao.Where(ra => ra.PerguntaAvaliacaoId == perguntaId).ToListAsync();
                id = perguntaAvaliacao.AvaliacaoId;
            }

            if (id != null)
            {
                viewModel.SelectedId = id.Value;
                Avaliacao avaliacao = viewModel.Avaliacoes.Single(a => a.Id == id.Value);
                viewModel.PerguntasAvaliacao = avaliacao.Perguntas.Where(pa => pa.AvaliacaoId == id.Value).ToList();
            }

            return View(viewModel);
        }
        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public IActionResult Create(int? moduloId)
        {
            if (moduloId != null)
            {
                Modulo modulo = _context.Modulos.Find(moduloId);

                ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome", modulo.CursoId);
                ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao", moduloId);
                ViewData["mId"] = moduloId;
            }
            else
            {
                ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
                ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao");
                ViewData["mId"] = 0;
            }

            return View();
        }
        public JsonResult GetModulosByCursoId(int id)
        {
            var list = _context.Modulos.Where(m => m.CursoId == id).ToList();
            list.Insert(0, new Modulo { Id = 0, Descricao = "-- Select Module --" });
            return Json(new SelectList(list, "Id", "Descricao"));
        }
        // POST: Avaliacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuloId,Descricao")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                var modulo = await _context.Modulos.FindAsync(avaliacao.ModuloId);
                modulo.AvaliacaoId = avaliacao.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            //ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao", avaliacao.ModuloId);
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao, CursoId, ModuloId")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
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
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
