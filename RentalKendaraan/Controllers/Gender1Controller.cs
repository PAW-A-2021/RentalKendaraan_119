﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan.Models;

namespace RentalKendaraan.Controllers
{
    public class Gender1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Gender1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Gender1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gender1s.ToListAsync());
        }

        // GET: Gender1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender1 = await _context.Gender1s
                .FirstOrDefaultAsync(m => m.IdGender == id);
            if (gender1 == null)
            {
                return NotFound();
            }

            return View(gender1);
        }

        // GET: Gender1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gender1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGender,NamaGender")] Gender1 gender1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender1);
        }

        // GET: Gender1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender1 = await _context.Gender1s.FindAsync(id);
            if (gender1 == null)
            {
                return NotFound();
            }
            return View(gender1);
        }

        // POST: Gender1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGender,NamaGender")] Gender1 gender1)
        {
            if (id != gender1.IdGender)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Gender1Exists(gender1.IdGender))
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
            return View(gender1);
        }

        // GET: Gender1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender1 = await _context.Gender1s
                .FirstOrDefaultAsync(m => m.IdGender == id);
            if (gender1 == null)
            {
                return NotFound();
            }

            return View(gender1);
        }

        // POST: Gender1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gender1 = await _context.Gender1s.FindAsync(id);
            _context.Gender1s.Remove(gender1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Gender1Exists(int id)
        {
            return _context.Gender1s.Any(e => e.IdGender == id);
        }
    }
}
