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
    public class DescontosController : Controller
    {
        private readonly UpFirstDbContext _context;

        public DescontosController(UpFirstDbContext context)
        {
            _context = context;
        }

        // GET: Descontos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Descontos.ToListAsync());
        }

        // GET: Descontos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // GET: Descontos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Descontos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Valor")] Desconto desconto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desconto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desconto);
        }

        // GET: Descontos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos.FindAsync(id);
            if (desconto == null)
            {
                return NotFound();
            }
            return View(desconto);
        }

        // POST: Descontos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Valor")] Desconto desconto)
        {
            if (id != desconto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desconto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescontoExists(desconto.Id))
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
            return View(desconto);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<JsonResult> GetCupom(decimal percentualDesconto)
        {
            string codigo = RandomString(6);

            // Enquanto ja existe cupom com este codigo
            while (await _context.Cupons.Where(c=>c.Codigo == codigo).SingleOrDefaultAsync() != null)
                codigo = RandomString(6);

            _context.Cupons.Add(new Cupom { 
                Codigo = codigo, 
                Data = DateTime.Now, 
                Desconto = percentualDesconto, 
                Utilizado = false, 
                Validade = DateTime.Now.AddMonths(3) 
            });

            await _context.SaveChangesAsync();

            return Json(new { codigo = codigo });
        }
        // GET: Descontos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desconto = await _context.Descontos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desconto == null)
            {
                return NotFound();
            }

            return View(desconto);
        }

        // POST: Descontos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var desconto = await _context.Descontos.FindAsync(id);
            _context.Descontos.Remove(desconto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescontoExists(int id)
        {
            return _context.Descontos.Any(e => e.Id == id);
        }
    }
}
