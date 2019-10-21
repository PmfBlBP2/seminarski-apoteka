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
    public class OsiguranikController : Controller
    {
        private readonly ApotekaContext _context;

        public OsiguranikController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Osiguranik
        public async Task<IActionResult> Index()
        {
            var apotekaContext = _context.Osiguranik.Include(o => o.Grad);
            return View(await apotekaContext.ToListAsync());
        }

        // GET: Osiguranik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osiguranik = await _context.Osiguranik
                .Include(o => o.Grad)
                .FirstOrDefaultAsync(m => m.OsiguranikId == id);
            if (osiguranik == null)
            {
                return NotFound();
            }

            return View(osiguranik);
        }

        // GET: Osiguranik/Create
        public IActionResult Create()
        {
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "GradId");
            return View();
        }

        // POST: Osiguranik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OsiguranikId,Jmbg,Ime,Prezime,GradId,Adresa,BrojTelefona")] Osiguranik osiguranik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osiguranik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "GradId", osiguranik.GradId);
            return View(osiguranik);
        }

        // GET: Osiguranik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osiguranik = await _context.Osiguranik.FindAsync(id);
            if (osiguranik == null)
            {
                return NotFound();
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "GradId", osiguranik.GradId);
            return View(osiguranik);
        }

        // POST: Osiguranik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OsiguranikId,Jmbg,Ime,Prezime,GradId,Adresa,BrojTelefona")] Osiguranik osiguranik)
        {
            if (id != osiguranik.OsiguranikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osiguranik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsiguranikExists(osiguranik.OsiguranikId))
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
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "GradId", osiguranik.GradId);
            return View(osiguranik);
        }

        // GET: Osiguranik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osiguranik = await _context.Osiguranik
                .Include(o => o.Grad)
                .FirstOrDefaultAsync(m => m.OsiguranikId == id);
            if (osiguranik == null)
            {
                return NotFound();
            }

            return View(osiguranik);
        }

        // POST: Osiguranik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osiguranik = await _context.Osiguranik.FindAsync(id);
            _context.Osiguranik.Remove(osiguranik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsiguranikExists(int id)
        {
            return _context.Osiguranik.Any(e => e.OsiguranikId == id);
        }
    }
}
