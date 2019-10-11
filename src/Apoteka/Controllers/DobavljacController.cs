﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apoteka.Models;

namespace Apoteka.Controllers
{
    public class DobavljacController : Controller
    {
        private readonly ApotekaContext _context;

        public DobavljacController(ApotekaContext context)
        {
            _context = context;
        }

        // GET: Dobavljac
        public async Task<IActionResult> Index()
        {
            var apotekaContext = _context.Dobavljac.Include(d => d.Grad);
            return View(await apotekaContext.ToListAsync());
        }

        // GET: Dobavljac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dobavljac = await _context.Dobavljac
                .Include(d => d.Grad)
                .FirstOrDefaultAsync(m => m.DobavljacId == id);
            if (dobavljac == null)
            {
                return NotFound();
            }

            return View(dobavljac);
        }

        // GET: Dobavljac/Create
        public IActionResult Create()
        {
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "Naziv");
            return View();
        }

        // POST: Dobavljac/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DobavljacId,GradId,Naziv,Adresa,Telefon,EMail")] Dobavljac dobavljac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dobavljac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "Naziv", dobavljac.GradId);
            return View(dobavljac);
        }

        // GET: Dobavljac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dobavljac = await _context.Dobavljac.FindAsync(id);
            if (dobavljac == null)
            {
                return NotFound();
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "Naziv", dobavljac.GradId);
            return View(dobavljac);
        }

        // POST: Dobavljac/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DobavljacId,GradId,Naziv,Adresa,Telefon,EMail")] Dobavljac dobavljac)
        {
            if (id != dobavljac.DobavljacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dobavljac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DobavljacExists(dobavljac.DobavljacId))
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
            ViewData["GradId"] = new SelectList(_context.Grad, "GradId", "Naziv", dobavljac.GradId);
            return View(dobavljac);
        }

        // GET: Dobavljac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dobavljac = await _context.Dobavljac
                .Include(d => d.Grad)
                .FirstOrDefaultAsync(m => m.DobavljacId == id);
            if (dobavljac == null)
            {
                return NotFound();
            }

            return View(dobavljac);
        }

        // POST: Dobavljac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dobavljac = await _context.Dobavljac.FindAsync(id);
            _context.Dobavljac.Remove(dobavljac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DobavljacExists(int id)
        {
            return _context.Dobavljac.Any(e => e.DobavljacId == id);
        }
    }
}
