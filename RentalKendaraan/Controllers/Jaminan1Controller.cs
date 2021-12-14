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
    public class Jaminan1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Jaminan1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Jaminan1
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Jaminan1s orderby d.NamaJaminan select d.NamaJaminan;

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Jaminan1s select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NamaJaminan == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NamaJaminan.Contains(searchString));

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
                    menu = menu.OrderByDescending(s => s.NamaJaminan);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaJaminan);
                    break;
            }

            return View(await PaginatedList<Jaminan1>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Jaminan1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jaminan1 = await _context.Jaminan1s
                .FirstOrDefaultAsync(m => m.IdJaminan == id);
            if (jaminan1 == null)
            {
                return NotFound();
            }

            return View(jaminan1);
        }

        // GET: Jaminan1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jaminan1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJaminan,NamaJaminan")] Jaminan1 jaminan1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jaminan1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jaminan1);
        }

        // GET: Jaminan1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jaminan1 = await _context.Jaminan1s.FindAsync(id);
            if (jaminan1 == null)
            {
                return NotFound();
            }
            return View(jaminan1);
        }

        // POST: Jaminan1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJaminan,NamaJaminan")] Jaminan1 jaminan1)
        {
            if (id != jaminan1.IdJaminan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jaminan1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Jaminan1Exists(jaminan1.IdJaminan))
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
            return View(jaminan1);
        }

        // GET: Jaminan1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jaminan1 = await _context.Jaminan1s
                .FirstOrDefaultAsync(m => m.IdJaminan == id);
            if (jaminan1 == null)
            {
                return NotFound();
            }

            return View(jaminan1);
        }

        // POST: Jaminan1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jaminan1 = await _context.Jaminan1s.FindAsync(id);
            _context.Jaminan1s.Remove(jaminan1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Jaminan1Exists(int id)
        {
            return _context.Jaminan1s.Any(e => e.IdJaminan == id);
        }
    }
}
