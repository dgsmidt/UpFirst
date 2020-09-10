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
    public class PerguntasAvaliacaoController : Controller
    {
        private readonly UpFirstDbContext _context;

        public PerguntasAvaliacaoController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: PerguntasAvaliacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.PerguntasAvaliacao
                .Include(pa => pa.Respostas)
                .ToListAsync());
        }

        // GET: PerguntasAvaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perguntaAvaliacao = await _context.PerguntasAvaliacao
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
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Descricao", avaliacaoId ?? -1);
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
                _context.Add(perguntaAvaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var perguntaAvaliacao = await _context.PerguntasAvaliacao.FindAsync(id);
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
                    _context.Update(perguntaAvaliacao);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
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

            var perguntaAvaliacao = await _context.PerguntasAvaliacao
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
            var perguntaAvaliacao = await _context.PerguntasAvaliacao.FindAsync(id);
            _context.PerguntasAvaliacao.Remove(perguntaAvaliacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerguntaAvaliacaoExists(int id)
        {
            return _context.PerguntasAvaliacao.Any(e => e.Id == id);
        }
    }
}
