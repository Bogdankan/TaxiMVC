using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaxiMVC.Data;
using TaxiMVC.Models;
using TaxiMVC.Models.Driver;
using static NuGet.Packaging.PackagingConstants;

namespace TaxiMVC.Controllers
{
    //[Authorize]
    public class DriversController : Controller
    {
        private readonly TaxiContext _context;

        public DriversController(TaxiContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(string? firstName = null, string? lastName = null, string? phoneNumber = null, DriversSortState sortOrder = DriversSortState.FirstNameAsc, int page = 1)
        {
            int pageSize = 3;
            IQueryable<Driver>? drivers = _context.Driver;
            var count = await drivers.CountAsync();
            drivers = drivers.Skip((page - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(firstName))
            {
                drivers = drivers.Where(x => x.FirstName.Contains(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                drivers = drivers.Where(x => x.LastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                drivers = drivers.Where(x => x.PhoneNumber.Contains(phoneNumber));
            }

            ViewData["FirstNameSort"] = sortOrder == DriversSortState.FirstNameAsc ? DriversSortState.FirstNameDesc : DriversSortState.FirstNameAsc;
            ViewData["SecondNameSort"] = sortOrder == DriversSortState.SecondNameAsc ? DriversSortState.SecondNameDesc : DriversSortState.SecondNameAsc;

            drivers = sortOrder switch
            {
                DriversSortState.FirstNameDesc => drivers.OrderByDescending(s => s.FirstName),
                DriversSortState.SecondNameAsc => drivers.OrderBy(s => s.LastName),
                DriversSortState.SecondNameDesc => drivers.OrderByDescending(s => s.LastName),
                _ => drivers.OrderBy(s => s.FirstName),
            };

            PageVM pageVM = new PageVM(count, page, pageSize);

            FilterDriverVM filterDriverVM = new FilterDriverVM
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,                
            };

            SortDriverVM sortDriverVM = new SortDriverVM(sortOrder);

            DriverIndexVM driverIndexVM = new DriverIndexVM
            {
                Drivers = await drivers.AsNoTracking().ToListAsync(),
                FilterDriverVM = filterDriverVM,
                SortDriverVM = sortDriverVM,
                PageVM = pageVM,
            };

            //IEnumerable<Driver> driversList = await drivers.AsNoTracking().ToListAsync();
            //DriverListVM driverListVM = new DriverListVM
            //{
            //    FirstName = firstName,
            //    LastName = secondName,
            //    PhoneNumber = phoneNumber,
            //    Drivers = driversList
            //};

            return View(driverIndexVM);
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Driver == null)
            {
                return NotFound();
            }

            var driver = await _context.Driver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Driver == null)
            {
                return Problem("Entity set 'TaxiContext.Driver'  is null.");
            }
            var driver = await _context.Driver.FindAsync(id);
            if (driver != null)
            {
                _context.Driver.Remove(driver);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
          return (_context.Driver?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
