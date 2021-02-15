using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using Upfirst.ViewModels;

namespace Upfirst.Controllers
{
    public class PerguntasAvaliacaoController : Controller
    {
        private readonly UpFirstDbContext _dbContext;

        public PerguntasAvaliacaoController(UpFirstDbContext context)
        {
            _dbContext = context;
        }

        // GET: PerguntasAvaliacao
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new PerguntasAvaliacaoVM();

            viewModel.PerguntasAvaliacao = await _dbContext.PerguntasAvaliacao
                .Include(p => p.Respostas)
                .AsNoTracking()
                .OrderBy(p => p.Descricao)
                .ToListAsync();

            if (id != null)
            {
                var perguntaId = id.Value;
                PerguntaAvaliacao perguntaAvaliacao = viewModel.PerguntasAvaliacao.Single(pa => pa.Id == perguntaId);
                viewModel.RespostasAvaliacao = perguntaAvaliacao.Respostas.Where(ra => ra.PerguntaAvaliacaoId == perguntaId).ToList();
                viewModel.SelectedId = perguntaId;
                viewModel.AvaliacaoId = perguntaAvaliacao.AvaliacaoId;
            }

            return View(viewModel);
        }

        // GET: PerguntasAvaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAvaliacao = await _dbContext.PerguntasAvaliacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perguntaAvaliacao == null)
            {
                return NotFound();
            }

            return View(perguntaAvaliacao);
        }

        // GET: PerguntasAvaliacao/Create
        public IActionResult Create(int? avaliacaoId)
        {
            if (avaliacaoId != null)
            {
                ViewData["AvaliacaoId"] = new SelectList(_dbContext.Avaliacoes.Where(a => a.Id == avaliacaoId), "Id", "Descricao", avaliacaoId ?? -1);
            }
            else
            {
                ViewData["AvaliacaoId"] = new SelectList(_dbContext.Avaliacoes, "Id", "Descricao", avaliacaoId ?? -1);
            }

            //var model = new PerguntaAvaliacao { AvaliacaoId = avaliacaoId ?? -1 };

            return View();
        }

        // POST: PerguntasAvaliacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AvaliacaoId,Descricao")] PerguntaAvaliacao perguntaAvaliacao)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(perguntaAvaliacao);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Avaliacoes", new { perguntaId = perguntaAvaliacao.Id });
            }
            return View(perguntaAvaliacao);
        }

        // GET: PerguntasAvaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAvaliacao = await _dbContext.PerguntasAvaliacao.FindAsync(id);
            if (perguntaAvaliacao == null)
            {
                return NotFound();
            }
            return View(perguntaAvaliacao);
        }

        // POST: PerguntasAvaliacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,AvaliacaoId")] PerguntaAvaliacao perguntaAvaliacao)
        {
            if (id != perguntaAvaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(perguntaAvaliacao);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerguntaAvaliacaoExists(perguntaAvaliacao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Avaliacoes", new { perguntaId = perguntaAvaliacao.Id });
            }
            return View(perguntaAvaliacao);
        }

        // GET: PerguntasAvaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAvaliacao = await _dbContext.PerguntasAvaliacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perguntaAvaliacao == null)
            {
                return NotFound();
            }

            return View(perguntaAvaliacao);
        }

        // POST: PerguntasAvaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perguntaAvaliacao = await _dbContext.PerguntasAvaliacao.FindAsync(id);
            _dbContext.PerguntasAvaliacao.Remove(perguntaAvaliacao);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Avaliacoes", new { perguntaId = id });
        }

        private bool PerguntaAvaliacaoExists(int id)
        {
            return _dbContext.PerguntasAvaliacao.Any(e => e.Id == id);
        }
    }
}
