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
    public class Peminjaman1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Peminjaman1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Peminjaman1
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Peminjaman1s orderby d.Biaya.ToString() select d.Biaya.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Peminjaman1s.Include(p => p.IdCustomerNavigation).
                       Include(p => p.IdJaminanNavigation).Include(p => p.IdKendaraanNavigation)
                       select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.Biaya.ToString() == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.IdCustomerNavigation.NamaCustomer.Contains(searchString) || s.IdJaminanNavigation.NamaJaminan.Contains(searchString)
                || s.IdKendaraanNavigation.NamaKendaraan.Contains(searchString) || s.TglPeminjaman.ToString().Contains(searchString));
            }

            return View(await menu.ToListAsync());
        }

        // GET: Peminjaman1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman1 = await _context.Peminjaman1s
                .Include(p => p.IdCustomerNavigation)
                .Include(p => p.IdJaminanNavigation)
                .Include(p => p.IdKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (peminjaman1 == null)
            {
                return NotFound();
            }

            return View(peminjaman1);
        }

        // GET: Peminjaman1/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customer1s, "IdCustomer", "Alamat");
            ViewData["IdJaminan"] = new SelectList(_context.Jaminan1s, "IdJaminan", "NamaJaminan");
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraan1s, "IdKendaraan", "Ketersediaan");
            return View();
        }

        // POST: Peminjaman1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Peminjaman1 peminjaman1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peminjaman1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customer1s, "IdCustomer", "Alamat", peminjaman1.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminan1s, "IdJaminan", "NamaJaminan", peminjaman1.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraan1s, "IdKendaraan", "Ketersediaan", peminjaman1.IdKendaraan);
            return View(peminjaman1);
        }

        // GET: Peminjaman1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman1 = await _context.Peminjaman1s.FindAsync(id);
            if (peminjaman1 == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customer1s, "IdCustomer", "Alamat", peminjaman1.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminan1s, "IdJaminan", "NamaJaminan", peminjaman1.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraan1s, "IdKendaraan", "Ketersediaan", peminjaman1.IdKendaraan);
            return View(peminjaman1);
        }

        // POST: Peminjaman1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Peminjaman1 peminjaman1)
        {
            if (id != peminjaman1.IdPeminjaman)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peminjaman1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Peminjaman1Exists(peminjaman1.IdPeminjaman))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customer1s, "IdCustomer", "Alamat", peminjaman1.IdCustomer);
            ViewData["IdJaminan"] = new SelectList(_context.Jaminan1s, "IdJaminan", "NamaJaminan", peminjaman1.IdJaminan);
            ViewData["IdKendaraan"] = new SelectList(_context.Kendaraan1s, "IdKendaraan", "Ketersediaan", peminjaman1.IdKendaraan);
            return View(peminjaman1);
        }

        // GET: Peminjaman1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peminjaman1 = await _context.Peminjaman1s
                .Include(p => p.IdCustomerNavigation)
                .Include(p => p.IdJaminanNavigation)
                .Include(p => p.IdKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (peminjaman1 == null)
            {
                return NotFound();
            }

            return View(peminjaman1);
        }

        // POST: Peminjaman1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peminjaman1 = await _context.Peminjaman1s.FindAsync(id);
            _context.Peminjaman1s.Remove(peminjaman1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Peminjaman1Exists(int id)
        {
            return _context.Peminjaman1s.Any(e => e.IdPeminjaman == id);
        }
    }
}
