using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly PersonaDbContext _context;

        public EstudiosController(PersonaDbContext context)
        {
            _context = context;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            var personaDbContext = _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation);
            return View(await personaDbContext.ToListAsync());
        }

        // GET: Estudios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == id);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        public IActionResult Create()
        {
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id");
            return View();
        }

        // POST: Estudios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {

            bool profPerExiste = await _context.Estudios
                .AnyAsync(p => p.IdProf == estudio.IdProf && p.CcPer == estudio.CcPer);

            // Si el teléfono ya existe, se muestra en la pantalla
            if (profPerExiste)
            {
                Console.WriteLine("El prof con per ya está");
                ModelState.AddModelError("CcPer", "La profesión para esa persona ya existe.");
            }
            else
            {
                var profesion = await _context.Profesions.FirstOrDefaultAsync(m =>
                    m.Id == estudio.IdProf
                );
                estudio.IdProfNavigation = profesion;

                var persona = await _context.Personas.FirstOrDefaultAsync(m =>
                     m.Cc == estudio.CcPer
                );
                estudio.CcPerNavigation = persona;

                ModelState.Clear();
                TryValidateModel(estudio);

                if (ModelState.IsValid)
                {
                    _context.Add(estudio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }

            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Edit/5
        public async Task<IActionResult> Edit(int? IdProf, int? CcPer)
        {
            if (IdProf == null || CcPer == null)
            {
                return NotFound();
            }
            var estudio = await _context.Estudios
            .FirstOrDefaultAsync(e => e.IdProf == IdProf && e.CcPer == CcPer);

            if (estudio == null)
            {
                return NotFound();
            }
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // POST: Estudios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdProf, int CcPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (IdProf != estudio.IdProf || CcPer != estudio.CcPer)
            {
                return NotFound();
            }

            var profesion = await _context.Profesions.FirstOrDefaultAsync(m =>
                    m.Id == estudio.IdProf
                );
            estudio.IdProfNavigation = profesion;

            var persona = await _context.Personas.FirstOrDefaultAsync(m =>
                 m.Cc == estudio.CcPer
            );
            estudio.CcPerNavigation = persona;

            ModelState.Clear();
            TryValidateModel(estudio);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudioExists(estudio.IdProf, estudio.CcPer))
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
            ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
            return View(estudio);
        }

        // GET: Estudios/Delete/5
        public async Task<IActionResult> Delete(int? Idprof, int? CcPer)
        {
            if (Idprof == null || CcPer == null)
            {
                return NotFound();
            }

            var estudio = await _context.Estudios
                .Include(e => e.CcPerNavigation)
                .Include(e => e.IdProfNavigation)
                .FirstOrDefaultAsync(m => m.IdProf == Idprof && m.CcPer == CcPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdProf, int CcPer)
        {
            var estudio = await _context.Estudios
                .FirstOrDefaultAsync(e => e.IdProf == IdProf && e.CcPer == CcPer);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudioExists(int IdProf, int CcPer )
        {
            return _context.Estudios.Any(e => e.IdProf == IdProf && e.CcPer == CcPer);
        }
    }
}
