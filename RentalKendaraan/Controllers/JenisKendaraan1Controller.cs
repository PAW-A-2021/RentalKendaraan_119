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
    public class JenisKendaraan1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public JenisKendaraan1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: JenisKendaraan1
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.JenisKendaraan1s orderby d.NamaJenisKendaraan select d.NamaJenisKendaraan;

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.JenisKendaraan1s select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NamaJenisKendaraan == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NamaJenisKendaraan.Contains(searchString));
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

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaJenisKendaraan);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaJenisKendaraan);
                    break;
            }

            return View(await PaginatedList<JenisKendaraan1>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: JenisKendaraan1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisKendaraan1 = await _context.JenisKendaraan1s
                .FirstOrDefaultAsync(m => m.IdJenisKendaraan == id);
            if (jenisKendaraan1 == null)
            {
                return NotFound();
            }

            return View(jenisKendaraan1);
        }

        // GET: JenisKendaraan1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JenisKendaraan1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJenisKendaraan,NamaJenisKendaraan")] JenisKendaraan1 jenisKendaraan1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jenisKendaraan1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jenisKendaraan1);
        }

        // GET: JenisKendaraan1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisKendaraan1 = await _context.JenisKendaraan1s.FindAsync(id);
            if (jenisKendaraan1 == null)
            {
                return NotFound();
            }
            return View(jenisKendaraan1);
        }

        // POST: JenisKendaraan1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJenisKendaraan,NamaJenisKendaraan")] JenisKendaraan1 jenisKendaraan1)
        {
            if (id != jenisKendaraan1.IdJenisKendaraan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jenisKendaraan1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JenisKendaraan1Exists(jenisKendaraan1.IdJenisKendaraan))
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
            return View(jenisKendaraan1);
        }

        // GET: JenisKendaraan1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisKendaraan1 = await _context.JenisKendaraan1s
                .FirstOrDefaultAsync(m => m.IdJenisKendaraan == id);
            if (jenisKendaraan1 == null)
            {
                return NotFound();
            }

            return View(jenisKendaraan1);
        }

        // POST: JenisKendaraan1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jenisKendaraan1 = await _context.JenisKendaraan1s.FindAsync(id);
            _context.JenisKendaraan1s.Remove(jenisKendaraan1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JenisKendaraan1Exists(int id)
        {
            return _context.JenisKendaraan1s.Any(e => e.IdJenisKendaraan == id);
        }
    }
}
