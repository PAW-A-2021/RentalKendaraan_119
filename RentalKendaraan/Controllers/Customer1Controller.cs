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
    public class Customer1Controller : Controller
    {
        private readonly Rental_Kendaraan_ItasContext _context;

        public Customer1Controller(Rental_Kendaraan_ItasContext context)
        {
            _context = context;
        }

        // GET: Customer1
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Customer1s orderby d.IdGender select d.IdGender.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());
            ViewBag.ktsd = new SelectList(ktsdList);
            var menu = from m in _context.Customer1s.Include(k => k.IdGenderNavigation) select m;

            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.IdGender.ToString() == ktsd);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.Alamat.Contains(searchString) || s.NamaCustomer.Contains(searchString)
                || s.IdGender.ToString().Contains(searchString) || s.Nik.Contains(searchString) || s.NoHp.Contains(searchString));
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
                    menu = menu.OrderByDescending(s => s.NamaCustomer);
                    break;
                case "Date":
                    menu = menu.OrderBy(s => s.IdGenderNavigation.NamaGender);
                    break;
                case "date_desc":
                    menu = menu.OrderByDescending(s => s.IdGenderNavigation.NamaGender);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaCustomer);
                    break;
            }

            return View(await PaginatedList<Customer1>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Customer1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer1 = await _context.Customer1s
                .Include(c => c.IdGenderNavigation)
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (customer1 == null)
            {
                return NotFound();
            }

            return View(customer1);
        }

        // GET: Customer1/Create
        public IActionResult Create()
        {
            ViewData["IdGender"] = new SelectList(_context.Gender1s, "IdGender", "IdGender");
            return View();
        }

        // POST: Customer1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCustomer,NamaCustomer,Nik,Alamat,NoHp,IdGender")] Customer1 customer1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGender"] = new SelectList(_context.Gender1s, "IdGender", "IdGender", customer1.IdGender);
            return View(customer1);
        }

        // GET: Customer1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer1 = await _context.Customer1s.FindAsync(id);
            if (customer1 == null)
            {
                return NotFound();
            }
            ViewData["IdGender"] = new SelectList(_context.Gender1s, "IdGender", "IdGender", customer1.IdGender);
            return View(customer1);
        }

        // POST: Customer1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCustomer,NamaCustomer,Nik,Alamat,NoHp,IdGender")] Customer1 customer1)
        {
            if (id != customer1.IdCustomer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Customer1Exists(customer1.IdCustomer))
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
            ViewData["IdGender"] = new SelectList(_context.Gender1s, "IdGender", "IdGender", customer1.IdGender);
            return View(customer1);
        }

        // GET: Customer1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer1 = await _context.Customer1s
                .Include(c => c.IdGenderNavigation)
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (customer1 == null)
            {
                return NotFound();
            }

            return View(customer1);
        }

        // POST: Customer1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer1 = await _context.Customer1s.FindAsync(id);
            _context.Customer1s.Remove(customer1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Customer1Exists(int id)
        {
            return _context.Customer1s.Any(e => e.IdCustomer == id);
        }
    }
}
