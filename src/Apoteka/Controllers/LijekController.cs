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
    public class LijekController : Controller
    {
        private readonly ApotekaContext _context;

        public LijekController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Lijek
        public async Task<IActionResult> Index()
        {
            var apotekaContext = _context.Lijek.Include(l => l.Dobavljac);
            return View(await apotekaContext.ToListAsync());
        }

        // GET: Lijek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lijek = await _context.Lijek
                .Include(l => l.Dobavljac)
                .FirstOrDefaultAsync(m => m.LijekId == id);
            if (lijek == null)
            {
                return NotFound();
            }

            return View(lijek);
        }

        // GET: Lijek/Create
        public IActionResult Create()
        {
            ViewData["DobavljacId"] = new SelectList(_context.Dobavljac, "DobavljacId", "Naziv");
            return View();
        }

        // POST: Lijek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LijekId,DobavljacId,Naziv,NaRecept,Cijena,Kolicina")] Lijek lijek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lijek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DobavljacId"] = new SelectList(_context.Dobavljac, "DobavljacId", "DobavljacId", lijek.DobavljacId);
            return View(lijek);
        }

        // GET: Lijek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lijek = await _context.Lijek.FindAsync(id);
            if (lijek == null)
            {
                return NotFound();
            }
            ViewData["DobavljacId"] = new SelectList(_context.Dobavljac, "DobavljacId", "DobavljacId", lijek.DobavljacId);
            return View(lijek);
        }

        // POST: Lijek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LijekId,DobavljacId,Naziv,NaRecept,Cijena,Kolicina")] Lijek lijek)
        {
            if (id != lijek.LijekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lijek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LijekExists(lijek.LijekId))
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
            ViewData["DobavljacId"] = new SelectList(_context.Dobavljac, "DobavljacId", "DobavljacId", lijek.DobavljacId);
            return View(lijek);
        }

        // GET: Lijek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lijek = await _context.Lijek
                .Include(l => l.Dobavljac)
                .FirstOrDefaultAsync(m => m.LijekId == id);
            if (lijek == null)
            {
                return NotFound();
            }

            return View(lijek);
        }

        // POST: Lijek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lijek = await _context.Lijek.FindAsync(id);
            _context.Lijek.Remove(lijek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LijekExists(int id)
        {
            return _context.Lijek.Any(e => e.LijekId == id);
        }
    }
}
