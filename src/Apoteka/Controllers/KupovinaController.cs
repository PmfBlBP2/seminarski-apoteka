using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apoteka.Models;

namespace Apoteka.Controllers
{
    public class KupovinaController : Controller
    {
        private readonly ApotekaContext _context;

        public KupovinaController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Kupovina
        public async Task<IActionResult> Index()
        {
            var apotekaContext = _context.Kupovina.Include(k => k.Lijek).Include(k => k.Racun);
            return View(await apotekaContext.ToListAsync());
        }

        // GET: Kupovina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupovina = await _context.Kupovina
                .Include(k => k.Lijek)
                .Include(k => k.Racun)
                .FirstOrDefaultAsync(m => m.RacunId == id);
            if (kupovina == null)
            {
                return NotFound();
            }

            return View(kupovina);
        }

        // GET: Kupovina/Create
        public IActionResult Create()
        {
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "LijekId");
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId");
            return View();
        }

        // POST: Kupovina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RacunId,LijekId,Kolicina,Iznos")] Kupovina kupovina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kupovina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "LijekId", kupovina.LijekId);
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId", kupovina.RacunId);
            return View(kupovina);
        }

        // GET: Kupovina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupovina = await _context.Kupovina.FindAsync(id);
            if (kupovina == null)
            {
                return NotFound();
            }
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "LijekId", kupovina.LijekId);
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId", kupovina.RacunId);
            return View(kupovina);
        }

        // POST: Kupovina/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RacunId,LijekId,Kolicina,Iznos")] Kupovina kupovina)
        {
            if (id != kupovina.RacunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kupovina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KupovinaExists(kupovina.RacunId))
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
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "LijekId", kupovina.LijekId);
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId", kupovina.RacunId);
            return View(kupovina);
        }

        // GET: Kupovina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kupovina = await _context.Kupovina
                .Include(k => k.Lijek)
                .Include(k => k.Racun)
                .FirstOrDefaultAsync(m => m.RacunId == id);
            if (kupovina == null)
            {
                return NotFound();
            }

            return View(kupovina);
        }

        // POST: Kupovina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kupovina = await _context.Kupovina.FindAsync(id);
            _context.Kupovina.Remove(kupovina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KupovinaExists(int id)
        {
            return _context.Kupovina.Any(e => e.RacunId == id);
        }
    }
}
