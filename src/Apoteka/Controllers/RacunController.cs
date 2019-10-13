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
    public class RacunController : Controller
    {
        private readonly ApotekaContext _context;

        public RacunController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Racun
        public async Task<IActionResult> Index()
        {
            return View(await _context.Racun.ToListAsync());
        }

        // GET: Racun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun
                .FirstOrDefaultAsync(m => m.RacunId == id);
            if (racun == null)
            {
                return NotFound();
            }

            return View(racun);
        }

        // GET: Racun/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Racun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RacunId,Iznos,DatumIzdavanja,Jmbg")] Racun racun)
        {
            if (ModelState.IsValid)
            {
                racun.Iznos = 0;
                racun.DatumIzdavanja = DateTime.Today;
                _context.Add(racun);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Kupovina", new { racunId = racun.RacunId });
            }
            return View(racun);
        }

        // GET: Racun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun.FindAsync(id);
            if (racun == null)
            {
                return NotFound();
            }
            return View(racun);
        }

        // POST: Racun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RacunId,Iznos,DatumIzdavanja,Jmbg")] Racun racun)
        {
            if (id != racun.RacunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(racun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RacunExists(racun.RacunId))
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
            return View(racun);
        }

        // GET: Racun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var racun = await _context.Racun
                .FirstOrDefaultAsync(m => m.RacunId == id);
            if (racun == null)
            {
                return NotFound();
            }

            return View(racun);
        }

        // POST: Racun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var racun = await _context.Racun.FindAsync(id);
            _context.Racun.Remove(racun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RacunExists(int id)
        {
            return _context.Racun.Any(e => e.RacunId == id);
        }
    }
}
