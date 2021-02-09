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
    public class CuponsController : Controller
    {
        private readonly UpFirstDbContext _context;

        public CuponsController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: Cupons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cupons.ToListAsync());
        }

        // GET: Cupons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupom == null)
            {
                return NotFound();
            }

            return View(cupom);
        }

        // GET: Cupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Data,Validade,Desconto,Utilizado")] Cupom cupom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cupom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cupom);
        }

        // GET: Cupons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons.FindAsync(id);
            if (cupom == null)
            {
                return NotFound();
            }
            return View(cupom);
        }

        // POST: Cupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Data,Validade,Desconto,Utilizado")] Cupom cupom)
        {
            if (id != cupom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cupom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CupomExists(cupom.Id))
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
            return View(cupom);
        }

        // GET: Cupons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupom = await _context.Cupons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupom == null)
            {
                return NotFound();
            }

            return View(cupom);
        }

        // POST: Cupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupom = await _context.Cupons.FindAsync(id);
            _context.Cupons.Remove(cupom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CupomExists(int id)
        {
            return _context.Cupons.Any(e => e.Id == id);
        }
    }
}
