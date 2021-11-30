using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan.Models;

namespace RentalKendaraan.Controllers
{
    public class KondisiKendaraan1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public KondisiKendaraan1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: KondisiKendaraan1
        public async Task<IActionResult> Index()
        {
            return View(await _context.KondisiKendaraan1s.ToListAsync());
        }

        // GET: KondisiKendaraan1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan1 = await _context.KondisiKendaraan1s
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisiKendaraan1 == null)
            {
                return NotFound();
            }

            return View(kondisiKendaraan1);
        }

        // GET: KondisiKendaraan1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KondisiKendaraan1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKondisi,NamaKondisi")] KondisiKendaraan1 kondisiKendaraan1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kondisiKendaraan1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kondisiKendaraan1);
        }

        // GET: KondisiKendaraan1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan1 = await _context.KondisiKendaraan1s.FindAsync(id);
            if (kondisiKendaraan1 == null)
            {
                return NotFound();
            }
            return View(kondisiKendaraan1);
        }

        // POST: KondisiKendaraan1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKondisi,NamaKondisi")] KondisiKendaraan1 kondisiKendaraan1)
        {
            if (id != kondisiKendaraan1.IdKondisi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kondisiKendaraan1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KondisiKendaraan1Exists(kondisiKendaraan1.IdKondisi))
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
            return View(kondisiKendaraan1);
        }

        // GET: KondisiKendaraan1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisiKendaraan1 = await _context.KondisiKendaraan1s
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisiKendaraan1 == null)
            {
                return NotFound();
            }

            return View(kondisiKendaraan1);
        }

        // POST: KondisiKendaraan1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kondisiKendaraan1 = await _context.KondisiKendaraan1s.FindAsync(id);
            _context.KondisiKendaraan1s.Remove(kondisiKendaraan1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KondisiKendaraan1Exists(int id)
        {
            return _context.KondisiKendaraan1s.Any(e => e.IdKondisi == id);
        }
    }
}
