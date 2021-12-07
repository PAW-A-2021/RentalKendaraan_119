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
    public class Pengembalian1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Pengembalian1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Pengembalian1
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Pengembalian1s orderby d.Denda.ToString() select d.Denda.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Pengembalian1s.Include(p => p.IdKondisiNavigation).Include(p => p.IdPeminjamanNavigation) select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.Denda.ToString() == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.IdKondisiNavigation.NamaKondisi.Contains(searchString) ||
                s.IdPeminjamanNavigation.TglPeminjaman.ToString().Contains(searchString)
                || s.TglPengembalian.ToString().Contains(searchString));
            }

            return View(await menu.ToListAsync());
        }

        // GET: Pengembalian1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian1 = await _context.Pengembalian1s
                .Include(p => p.IdKondisiNavigation)
                .Include(p => p.IdPeminjamanNavigation)
                .FirstOrDefaultAsync(m => m.IdPengembalian == id);
            if (pengembalian1 == null)
            {
                return NotFound();
            }

            return View(pengembalian1);
        }

        // GET: Pengembalian1/Create
        public IActionResult Create()
        {
            ViewData["IdKondisi"] = new SelectList(_context.KondisiKendaraan1s, "IdKondisi", "NamaKondisi");
            ViewData["IdPeminjaman"] = new SelectList(_context.Peminjaman1s, "IdPeminjaman", "IdPeminjaman");
            return View();
        }

        // POST: Pengembalian1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPengembalian,TglPengembalian,IdPeminjaman,IdKondisi,Denda")] Pengembalian1 pengembalian1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pengembalian1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKondisi"] = new SelectList(_context.KondisiKendaraan1s, "IdKondisi", "NamaKondisi", pengembalian1.IdKondisi);
            ViewData["IdPeminjaman"] = new SelectList(_context.Peminjaman1s, "IdPeminjaman", "IdPeminjaman", pengembalian1.IdPeminjaman);
            return View(pengembalian1);
        }

        // GET: Pengembalian1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian1 = await _context.Pengembalian1s.FindAsync(id);
            if (pengembalian1 == null)
            {
                return NotFound();
            }
            ViewData["IdKondisi"] = new SelectList(_context.KondisiKendaraan1s, "IdKondisi", "NamaKondisi", pengembalian1.IdKondisi);
            ViewData["IdPeminjaman"] = new SelectList(_context.Peminjaman1s, "IdPeminjaman", "IdPeminjaman", pengembalian1.IdPeminjaman);
            return View(pengembalian1);
        }

        // POST: Pengembalian1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPengembalian,TglPengembalian,IdPeminjaman,IdKondisi,Denda")] Pengembalian1 pengembalian1)
        {
            if (id != pengembalian1.IdPengembalian)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pengembalian1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pengembalian1Exists(pengembalian1.IdPengembalian))
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
            ViewData["IdKondisi"] = new SelectList(_context.KondisiKendaraan1s, "IdKondisi", "NamaKondisi", pengembalian1.IdKondisi);
            ViewData["IdPeminjaman"] = new SelectList(_context.Peminjaman1s, "IdPeminjaman", "IdPeminjaman", pengembalian1.IdPeminjaman);
            return View(pengembalian1);
        }

        // GET: Pengembalian1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pengembalian1 = await _context.Pengembalian1s
                .Include(p => p.IdKondisiNavigation)
                .Include(p => p.IdPeminjamanNavigation)
                .FirstOrDefaultAsync(m => m.IdPengembalian == id);
            if (pengembalian1 == null)
            {
                return NotFound();
            }

            return View(pengembalian1);
        }

        // POST: Pengembalian1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pengembalian1 = await _context.Pengembalian1s.FindAsync(id);
            _context.Pengembalian1s.Remove(pengembalian1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pengembalian1Exists(int id)
        {
            return _context.Pengembalian1s.Any(e => e.IdPengembalian == id);
        }
    }
}
