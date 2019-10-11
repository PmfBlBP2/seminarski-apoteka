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
    public class GradController : Controller
    {
        private readonly ApotekaContext _context;

        public GradController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Grad
        public async Task<IActionResult> Index()
        {
            var apotekaContext = _context.Grad.Include(g => g.Drzava);
            return View(await apotekaContext.ToListAsync());
        }

        // GET: Grad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad
                .Include(g => g.Drzava)
                .FirstOrDefaultAsync(m => m.GradId == id);
            if (grad == null)
            {
                return NotFound();
            }

            return View(grad);
        }

        // GET: Grad/Create
        public IActionResult Create()
        {
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "DrzavaId", "Naziv");
            return View();
        }

        // POST: Grad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradId,DrzavaId,Naziv,PostanskiBroj")] Grad grad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "DrzavaId", "Naziv", grad.DrzavaId);
            return View(grad);
        }

        // GET: Grad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad.FindAsync(id);
            if (grad == null)
            {
                return NotFound();
            }
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "DrzavaId", "Naziv", grad.DrzavaId);
            return View(grad);
        }

        // POST: Grad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradId,DrzavaId,Naziv,PostanskiBroj")] Grad grad)
        {
            if (id != grad.GradId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradExists(grad.GradId))
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
            ViewData["DrzavaId"] = new SelectList(_context.Drzava, "DrzavaId", "Naziv", grad.DrzavaId);
            return View(grad);
        }

        // GET: Grad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad
                .Include(g => g.Drzava)
                .FirstOrDefaultAsync(m => m.GradId == id);
            if (grad == null)
            {
                return NotFound();
            }

            return View(grad);
        }

        // POST: Grad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grad = await _context.Grad.FindAsync(id);
            _context.Grad.Remove(grad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradExists(int id)
        {
            return _context.Grad.Any(e => e.GradId == id);
        }
    }
}
