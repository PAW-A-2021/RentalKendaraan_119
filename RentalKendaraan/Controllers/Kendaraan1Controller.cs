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
    public class Kendaraan1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Kendaraan1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Kendaraan1
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //query mengambil data 
            var ktsdQuery = from d in _context.Kendaraan1s orderby d.Ketersediaan select d.Ketersediaan;

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            var menu = from m in _context.Kendaraan1s.Include(k => k.IdJenisKendaraanNavigation) select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.Ketersediaan == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NoPolisi.Contains(searchString) || s.NamaKendaraan.Contains(searchString) || s.NoStnk.Contains(searchString));
            }

            //membuat pagedlist
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            int pageSize = 5;

            //untuk sorting
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaKendaraan);
                    break;
                case "Date":
                    menu = menu.OrderBy(s => s.IdJenisKendaraanNavigation.IdJenisKendaraan);
                    break;
                case "date_desc":
                    menu = menu.OrderByDescending(s => s.IdJenisKendaraanNavigation.IdJenisKendaraan);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaKendaraan);
                    break;
            }

            return View(await PaginatedList<Kendaraan1>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Kendaraan1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan1 = await _context.Kendaraan1s
                .Include(k => k.IdJenisKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdKendaraan == id);
            if (kendaraan1 == null)
            {
                return NotFound();
            }

            return View(kendaraan1);
        }

        // GET: Kendaraan1/Create
        public IActionResult Create()
        {
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraan1s, "IdJenisKendaraan", "NamaJenisKendaraan");
            return View();
        }

        // POST: Kendaraan1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKendaraan,NamaKendaraan,NoPolisi,NoStnk,IdJenisKendaraan,Ketersediaan")] Kendaraan1 kendaraan1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kendaraan1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraan1s, "IdJenisKendaraan", "NamaJenisKendaraan", kendaraan1.IdJenisKendaraan);
            return View(kendaraan1);
        }

        // GET: Kendaraan1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan1 = await _context.Kendaraan1s.FindAsync(id);
            if (kendaraan1 == null)
            {
                return NotFound();
            }
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraan1s, "IdJenisKendaraan", "NamaJenisKendaraan", kendaraan1.IdJenisKendaraan);
            return View(kendaraan1);
        }

        // POST: Kendaraan1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKendaraan,NamaKendaraan,NoPolisi,NoStnk,IdJenisKendaraan,Ketersediaan")] Kendaraan1 kendaraan1)
        {
            if (id != kendaraan1.IdKendaraan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kendaraan1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Kendaraan1Exists(kendaraan1.IdKendaraan))
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
            ViewData["IdJenisKendaraan"] = new SelectList(_context.JenisKendaraan1s, "IdJenisKendaraan", "NamaJenisKendaraan", kendaraan1.IdJenisKendaraan);
            return View(kendaraan1);
        }

        // GET: Kendaraan1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kendaraan1 = await _context.Kendaraan1s
                .Include(k => k.IdJenisKendaraanNavigation)
                .FirstOrDefaultAsync(m => m.IdKendaraan == id);
            if (kendaraan1 == null)
            {
                return NotFound();
            }

            return View(kendaraan1);
        }

        // POST: Kendaraan1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kendaraan1 = await _context.Kendaraan1s.FindAsync(id);
            _context.Kendaraan1s.Remove(kendaraan1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Kendaraan1Exists(int id)
        {
            return _context.Kendaraan1s.Any(e => e.IdKendaraan == id);
        }
    }
}
