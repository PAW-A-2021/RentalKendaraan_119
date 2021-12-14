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
    public class Gender1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Gender1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Gender1
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Gender1s orderby d.NamaGender select d.NamaGender.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Gender1s select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.NamaGender.ToString() == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.NamaGender.Contains(searchString));
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
                    menu = menu.OrderByDescending(s => s.NamaGender);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaGender);
                    break;
            }

            return View(await PaginatedList<Gender1>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
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
