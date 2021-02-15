using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using System.IO;
using Upfirst.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Upfirst.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Upfirst.Controllers
{
    public class ConfiguracoesController : Controller
    {
        private readonly UpFirstDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ConfiguracoesController(UpFirstDbContext context, IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
            _context = context;
        }

        // GET: Configuracoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configuracoes.SingleOrDefaultAsync());
        }

        // GET: Configuracoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // GET: Configuracoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,CabecalhoTexto1_Index,Texto1_Index,Logo,NotaDeCorte,Video_Index,CabecalhoTexto2_Index,Texto2_Index,CabecalhoTexto3_Index,Texto3_Index,TextoAlvo_Index,TextoGrafico_Index,TextoComputador_Index,EnderecoLinha1,EnderecoLinha2,EnderecoLinha3")] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracao);
        }

        // GET: Configuracoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes.FirstOrDefaultAsync();

            if (configuracao == null)
            {
                return NotFound();
            }

            FilesOnServer fs = new FilesOnServer(_hostingEnvironment);

            ViewData["Video"] = new SelectList(fs.GetFilesOnServer());

            return View(configuracao);
        }

        // POST: Configuracoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,CabecalhoTexto1_Index,Texto1_Index,Logo,NotaDeCorte,Video_Index,CabecalhoTexto2_Index,Texto2_Index,CabecalhoTexto3_Index,Texto3_Index,TextoAlvo_Index,TextoGrafico_Index,TextoComputador_Index,EnderecoLinha1,EnderecoLinha2,EnderecoLinha3,LogoBackground,EmailContato")] Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracaoExists(configuracao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            return View(configuracao);
        }
        public IActionResult UploadLogo()
        {
            var model = new UploadFile();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadLogo(UploadFile logoFile)
        {
            long size = logoFile.FormFile.Length;

            var uniqueFileName = GetUniqueFileName(logoFile.FormFile.FileName);

            //using (var stream = System.IO.File.Create(tempFileName))
            //{
            //    await uploadLogo.FormFile.CopyToAsync(stream);
            //}

            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "assets\\logos");

            var filePath = Path.Combine(uploadPath, uniqueFileName);
            await logoFile.FormFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

            var conf = await _context.Configuracoes.SingleOrDefaultAsync();

            conf.Logo = "/assets/logos/" + uniqueFileName;

            await _context.SaveChangesAsync();

            //return Ok(new { size });
            return View("Index", conf);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        // GET: Configuracoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // POST: Configuracoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuracao = await _context.Configuracoes.FindAsync(id);
            _context.Configuracoes.Remove(configuracao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracaoExists(int id)
        {
            return _context.Configuracoes.Any(e => e.Id == id);
        }
        
    }
}
