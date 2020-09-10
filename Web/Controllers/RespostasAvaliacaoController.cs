using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace Web.Controllers
{
    public class RespostasAvaliacaoController : Controller
    {
        private readonly UpFirstDbContext _context;

        public RespostasAvaliacaoController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: RespostasAvaliacao
        public async Task<IActionResult> Index()
        {
            var upFirstDbContext = _context.RespostasAvaliacao.Include(r => r.Pergunta);
            return View(await upFirstDbContext.ToListAsync());
        }

        // GET: RespostasAvaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaAvaliacao = await _context.RespostasAvaliacao
                .Include(r => r.Pergunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respostaAvaliacao == null)
            {
                return NotFound();
            }

            return View(respostaAvaliacao);
        }

        // GET: RespostasAvaliacao/Create
        public IActionResult Create(int? perguntaId)
        {
            ViewData["PerguntaAvaliacaoId"] = new SelectList(_context.PerguntasAvaliacao, "Id", "Descricao", perguntaId);
            return View();
        }

        // POST: RespostasAvaliacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerguntaAvaliacaoId,Descricao,Correta,Escolhida")] RespostaAvaliacao respostaAvaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(respostaAvaliacao);

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "PerguntasAvaliacao");
            }
            ViewData["PerguntaAvaliacaoId"] = new SelectList(_context.PerguntasAvaliacao, "Id", "Id", respostaAvaliacao.PerguntaAvaliacaoId);
            return View(respostaAvaliacao);
        }

        // GET: RespostasAvaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaAvaliacao = await _context.RespostasAvaliacao.FindAsync(id);
            if (respostaAvaliacao == null)
            {
                return NotFound();
            }
            ViewData["PerguntaAvaliacaoId"] = new SelectList(_context.PerguntasAvaliacao, "Id", "Descricao", respostaAvaliacao.PerguntaAvaliacaoId);
            return View(respostaAvaliacao);
        }

        // POST: RespostasAvaliacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PerguntaAvaliacaoId,Descricao,Correta,Escolhida")] RespostaAvaliacao respostaAvaliacao)
        {
            if (id != respostaAvaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(respostaAvaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespostaAvaliacaoExists(respostaAvaliacao.Id))
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
            ViewData["PerguntaAvaliacaoId"] = new SelectList(_context.PerguntasAvaliacao, "Id", "Id", respostaAvaliacao.PerguntaAvaliacaoId);
            return View(respostaAvaliacao);
        }

        // GET: RespostasAvaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaAvaliacao = await _context.RespostasAvaliacao
                .Include(r => r.Pergunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respostaAvaliacao == null)
            {
                return NotFound();
            }

            return View(respostaAvaliacao);
        }

        // POST: RespostasAvaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respostaAvaliacao = await _context.RespostasAvaliacao.FindAsync(id);
            _context.RespostasAvaliacao.Remove(respostaAvaliacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespostaAvaliacaoExists(int id)
        {
            return _context.RespostasAvaliacao.Any(e => e.Id == id);
        }
    }
}
