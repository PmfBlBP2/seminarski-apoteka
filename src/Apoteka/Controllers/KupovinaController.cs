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
        private decimal? popust = (decimal)0.25;

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

        public async Task<IActionResult> Cart(int? racunId)
        {
            var apotekaContext = _context.Kupovina
                .Include(k => k.Lijek)
                .Include(k => k.Racun)
                .Where(x => x.RacunId == racunId);
            ViewBag.RacunId = racunId;
            var racun = await _context.Racun.FindAsync(racunId);
            ViewBag.Iznos = racun.Iznos;
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
        public IActionResult Create(int? racunId)
        {
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "Naziv");
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId");
            return View();
        }

        // POST: Kupovina/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int racunId, [Bind("LijekId,Kolicina,Iznos")] Kupovina kupovina)
        {
            if (ModelState.IsValid)
            {
                var staraKupovina = await _context.Kupovina.FindAsync(racunId, kupovina.LijekId);
                var lijek = await _context.Lijek.FindAsync(kupovina.LijekId);
                var racun = await _context.Racun.FindAsync(racunId);
                var osiguranikJmbg = _context.Osiguranik
                    .Where(x => x.Jmbg == racun.Jmbg)
                    .Select(x => x.Jmbg)
                    .FirstOrDefault();

                if (staraKupovina != null)
                {
                    if (lijek.Kolicina >= kupovina.Kolicina)
                    {
                        decimal cijena = (decimal)(kupovina.Kolicina * lijek.Cijena);

                        if (lijek.NaRecept != 0 && osiguranikJmbg != null)
                        {
                            staraKupovina.Iznos += cijena - cijena * popust;
                            racun.Iznos += (cijena - cijena * popust);
                            _context.Update(racun);
                        }
                        else
                        {
                            staraKupovina.Iznos += cijena;
                            racun.Iznos += cijena;
                            _context.Update(racun);
                        }

                        staraKupovina.Kolicina += kupovina.Kolicina;
                        _context.Update(staraKupovina);


                        lijek.Kolicina -= kupovina.Kolicina;
                        _context.Update(lijek);


                        await _context.SaveChangesAsync();
                        return RedirectToAction("Cart", "Kupovina", new { racunId = staraKupovina.RacunId });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", $"Broj lijekova na stanju je: {lijek.Kolicina}");
                    }

                }
                else
                {
                    kupovina.RacunId = racunId;

                    if (lijek.Kolicina >= kupovina.Kolicina)
                    {
                        decimal cijena = (decimal)(kupovina.Kolicina * lijek.Cijena);

                        if (lijek.NaRecept != 0 && osiguranikJmbg != null)
                        {
                            kupovina.Iznos = cijena - cijena * popust;
                            racun.Iznos += (cijena - cijena * popust);
                            _context.Update(racun);
                        }
                        else
                        {
                            kupovina.Iznos = cijena;
                            racun.Iznos += cijena;
                            _context.Update(racun);
                        }

                        _context.Add(kupovina);


                        lijek.Kolicina -= kupovina.Kolicina;
                        _context.Update(lijek);


                        await _context.SaveChangesAsync();
                        return RedirectToAction("Cart", "Kupovina", new { racunId = kupovina.RacunId });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", $"Broj lijekova na stanju je: {lijek.Kolicina}");
                    }

                }
            }
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "Naziv", kupovina.LijekId);
            ViewData["RacunId"] = new SelectList(_context.Racun, "RacunId", "RacunId", kupovina.RacunId);
            return View(kupovina);
        }

        public async Task<IActionResult> RemoveLijek(int racunId, int lijekId)
        {
            var kupovina = await _context.Kupovina.FindAsync(racunId, lijekId);
            var lijek = await _context.Lijek.FindAsync(lijekId);
            var racun = await _context.Racun.FindAsync(racunId);

            lijek.Kolicina += kupovina.Kolicina;
            _context.Update(lijek);

            racun.Iznos -= kupovina.Iznos;
            _context.Update(racun);

            _context.Kupovina.Remove(kupovina);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart", "Kupovina", new { racunId = kupovina.RacunId });
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
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "Naziv", kupovina.LijekId);
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
            ViewData["LijekId"] = new SelectList(_context.Lijek, "LijekId", "Naziv", kupovina.LijekId);
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
