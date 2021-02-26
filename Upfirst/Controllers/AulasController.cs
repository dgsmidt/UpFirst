//using AspNetCore;
using DAL;
using DAL.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Upfirst.Models;
using Upfirst.Utilities;

namespace Upfirst.Controllers
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class AulasController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UpFirstDbContext _context;
        private readonly ISharedCultureLocalizer _loc;
        private readonly string culture;
        public AulasController(UpFirstDbContext context, ISharedCultureLocalizer loc, IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
            _context = context;
            _loc = loc;
            culture = System.Globalization.CultureInfo.CurrentCulture.Name;
        }

        // GET: Aulas
        public async Task<IActionResult> Index()
        {
            var aulas = await _context.Aulas
                .Include(a => a.Modulo)
                    .ThenInclude(m => m.Curso)
                .Include(a => a.ArquivosApoio)
                .ToListAsync();

            return View(aulas);
        }

        // GET: Aulas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas
                .Include(a => a.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // GET: Aulas/Create
        public IActionResult Create(int? moduloId)
        {
            if (moduloId != null)
            {
                Modulo modulo = _context.Modulos.Find(moduloId);

                ViewData["CursoId"] = new SelectList(_context.Cursos.Where(c => c.Id == modulo.CursoId).ToList(), "Id", "Nome");
                ViewData["ModuloId"] = new SelectList(_context.Modulos.Where(m => m.Id == moduloId).ToList(), "Id", "Descricao");
            }
            else
            {
                ViewData["ModuloId"] = new SelectList(new List<Modulo>(), "Id", "Descricao");
            }

            FilesOnServer fs = new FilesOnServer(_hostingEnvironment);

            ViewData["Video"] = new SelectList(fs.GetFilesOnServer());

            return View();
        }
        public JsonResult GetModulosByCursoId(int id)
        {
            var list = _context.Modulos.Where(m => m.CursoId == id).ToList();
            list.Insert(0, new Modulo { Id = 0, Descricao = _loc.GetLocalizedString(culture, "-- Select Module --") });
            return Json(new SelectList(list, "Id", "Descricao"));
        }
        // POST: Aulas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModuloId,NumeroAula,Descricao,Video,MaterialApoio,ArquivosApoio")] Aula aula)
        {
            if (ModelState.IsValid)
            {
                var seq = await _context.Aulas.Where(a => a.ModuloId == aula.ModuloId).OrderByDescending(a => a.NumeroAula).FirstOrDefaultAsync();


                if (aula.Video != null && aula.Video.Contains("https://youtu.be/"))
                {
                    string newPath = aula.Video.Replace("https://youtu.be/", "https://www.youtube.com/watch?v=");

                    aula.Video = newPath;
                }

                if (seq == null)
                    aula.NumeroAula = 1;
                else
                    aula.NumeroAula = seq.NumeroAula + 1;

                aula.Modulo = null;

                _context.Add(aula);

                await _context.SaveChangesAsync();

                Modulo modulo = _context.Modulos.Find(aula.ModuloId);
                return RedirectToAction("Index", "Cursos", new { id = modulo.CursoId, moduloId = aula.ModuloId });
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Nome");
            ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Descricao", aula.ModuloId);
            return View(aula);
        }

        private class VideoFile
        {
            public int Id { get; set; }
            public string FileName { get; set; }
        }
        // GET: Aulas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas
                .Include(a => a.ArquivosApoio)
                .Where(a => a.Id == id)
                .SingleOrDefaultAsync();

            if (aula == null)
            {
                return NotFound();
            }

            ViewData["ModuloId"] = new SelectList(_context.Modulos.Where(m => m.Id == aula.ModuloId), "Id", "Descricao", aula.ModuloId);

            FilesOnServer fs = new FilesOnServer(_hostingEnvironment);

            ViewData["Video"] = new SelectList(fs.GetFilesOnServer());

            return View(aula);
        }
        
        private long GetFileSize(string fileName)
        {
            FileInfo fi = new FileInfo(Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + fileName));
            return fi.Length;
        }
        public IActionResult Uploads()
        {
            return View();
        }
        public JsonResult DeleteFile(string file)
        {
            System.IO.File.Delete(Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + file));
            return Json("OK");
        }
        public string Upload()
        {
            var resultList = new List<UploadFilesResult>();

            FilesOnServer fs = new FilesOnServer(_hostingEnvironment);

            List<string> filesOnServer = fs.GetFilesOnServer();

            foreach (var item in filesOnServer)
            {
                UploadFilesResult uploadFiles = new UploadFilesResult();
                uploadFiles.name = item;
                uploadFiles.size = GetFileSize(item);
                //uploadFiles.type = "image/jpeg";
                uploadFiles.url = "/uploads/" + item;
                uploadFiles.deleteUrl = "/en/aulas/deletefile?file=" + item;
                uploadFiles.thumbnailUrl = "/uploads/" + item;
                uploadFiles.deleteType = "GET";

                resultList.Add(uploadFiles);
            }

            JsonFiles jFiles = new JsonFiles(resultList);

            var i = 0;

            foreach (var item in resultList)
            {
                jFiles.files[i] = item;
                i++;
            }

            string output = JsonConvert.SerializeObject(jFiles);

            return output;
        }
        [HttpPost]
        public async Task<string> Upload(IFormFile files)
        {
            var resultList = new List<UploadFilesResult>();

            string path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads/" + files.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }

            UploadFilesResult uploadFiles = new UploadFilesResult();
            uploadFiles.name = files.FileName;
            uploadFiles.size = files.Length;
            uploadFiles.type = "image/jpeg";
            uploadFiles.url = "/uploads/" + files.FileName;
            uploadFiles.deleteUrl = "/en/aulas/deletefile?file=" + files.FileName;
            uploadFiles.thumbnailUrl = "/uploads/" + files.FileName;
            uploadFiles.deleteType = "GET";

            resultList.Add(uploadFiles);

            JsonFiles jFiles = new JsonFiles(resultList);

            var i = 0;

            foreach (var item in resultList)
            {
                jFiles.files[i] = item;
                i++;
            }

            string output = JsonConvert.SerializeObject(jFiles);

            return output;
        }
        // POST: Aulas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModuloId,NumeroAula,Descricao,Video,MaterialApoio,ArquivosApoio")] Aula aula)
        {
            if (id != aula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //if (aulaVm.FormFile != null)
                //{
                //    aulaVm.Aula.Video = await UploadVideo(aulaVm.FormFile);
                //}

                List<ArquivoApoio> arquivosApoio = await _context.ArquivosApoio.Where(aa => aa.AulaId == aula.Id).ToListAsync();

                _context.ArquivosApoio.RemoveRange(arquivosApoio);

                if (aula.Video != null && aula.Video.Contains("https://youtu.be/"))
                {
                    string newPath = aula.Video.Replace("https://youtu.be/", "https://www.youtube.com/watch?v=");

                    aula.Video = newPath;
                }

                try
                {
                    _context.Update(aula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AulaExists(aula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Modulo modulo = _context.Modulos.Find(aula.ModuloId);

                return RedirectToAction("Index", "Cursos", new { id = modulo.CursoId, moduloId = modulo.Id });
            }

            ViewData["ModuloId"] = new SelectList(_context.Modulos, "Id", "Id", aula.ModuloId);

            return View(aula);
        }
        //private async Task<string> UploadVideo(IFormFile formFile)
        //{
        //    long size = formFile.Length;

        //    var uniqueFileName = GetUniqueFileName(formFile.FileName);

        //    var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "aulas");

        //    var filePath = Path.Combine(uploadPath, uniqueFileName);
        //    await formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

        //    return "/aulas/" + uniqueFileName;

        //}
        // GET: Aulas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aula = await _context.Aulas
                .Include(a => a.Modulo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aula == null)
            {
                return NotFound();
            }

            return View(aula);
        }

        // POST: Aulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            Modulo modulo = _context.Modulos.Find(aula.ModuloId);
            return RedirectToAction("Index", "Cursos", new { id = modulo.CursoId, moduloId = modulo.Id });
        }
        //public IActionResult UploadAula(int? aulaId)
        //{
        //    var model = new UploadFile();

        //    model.Id = aulaId;
        //    return View(model);
        //}
        //[HttpPost]
        //public async Task<IActionResult> UploadAula(UploadFile aulaFile)
        //{
        //    long size = aulaFile.FormFile.Length;

        //    var uniqueFileName = GetUniqueFileName(aulaFile.FormFile.FileName);

        //    //using (var stream = System.IO.File.Create(tempFileName))
        //    //{
        //    //    await uploadLogo.FormFile.CopyToAsync(stream);
        //    //}

        //    var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "aulas");

        //    var filePath = Path.Combine(uploadPath, uniqueFileName);
        //    await aulaFile.FormFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

        //    Aula aula = await _context.Aulas.FindAsync(aulaFile.Id);

        //    aula.Video = "/aulas/" + uniqueFileName;

        //    await _context.SaveChangesAsync();

        //    IEnumerable<Aula> aulas = await _context.Aulas.ToListAsync();
        //    //return Ok(new { size });
        //    return View("Index", aulas);
        //}
        //private string GetUniqueFileName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //              + "_"
        //              + Guid.NewGuid().ToString().Substring(0, 4)
        //              + Path.GetExtension(fileName);
        //}
        private bool AulaExists(int id)
        {
            return _context.Aulas.Any(e => e.Id == id);
        }
    }
}
