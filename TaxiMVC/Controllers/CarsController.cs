using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaxiMVC.Data;
using TaxiMVC.Models;
using TaxiMVC.Models.Car;
using TaxiMVC.Models.Driver;
using TaxiMVC.Models.Order;
using static NuGet.Packaging.PackagingConstants;

namespace TaxiMVC.Controllers
{
    public class CarsController : Controller
    {
        private readonly TaxiContext _context;

        public CarsController(TaxiContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string? carClass = null, int? driver = null, CarsSortState sortOrder = CarsSortState.ClassAsc, int page = 1)
        {
            int pageSize = 3;
            IQueryable<Car>? cars = _context.Car.Include(x => x.Driver);
            var count = await cars.CountAsync();
            cars = cars.Skip((page - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(carClass))
            {
                cars = cars.Where(x => x.Class.Contains(carClass));
            }           
            if (driver != null && driver != 0)
            {
                cars = cars.Where(x => x.DriverId == driver);
            }

            List<Driver> drivers = await _context.Driver.ToListAsync();
            drivers.Insert(0, new Driver { FirstName = "Всі", Id = 0 });

            ViewData["ClassSort"] = sortOrder == CarsSortState.ClassAsc ? CarsSortState.ClassDesc : CarsSortState.ClassAsc;
            ViewData["DriverSort"] = sortOrder == CarsSortState.DriverAsc ? CarsSortState.DriverDesc : CarsSortState.DriverAsc;

            cars = sortOrder switch
            {
                CarsSortState.ClassDesc => cars.OrderByDescending(x => x.Class),
                CarsSortState.DriverAsc => cars.OrderBy(x => x.Driver.FirstName),
                CarsSortState.DriverDesc => cars.OrderByDescending(x => x.Driver.FirstName),
                _ => cars.OrderBy(x => x.Class)
            };

            PageVM pageVM = new PageVM(count, page, pageSize);

            FilterCarVM filterCarVM = new FilterCarVM
            {
                Class = carClass,
                DriverId = driver,
                Drivers = new SelectList(drivers, "Id", "FirstName")
            };

            SortCarVM sortCarVM = new SortCarVM(sortOrder);

            CarIndexVM carIndexVM = new CarIndexVM
            {
                Cars = await cars.AsNoTracking().ToListAsync(),
                FilterCarVM = filterCarVM,
                SortCarVM = sortCarVM,
                PageVM = pageVM,
            };

            return View(carIndexVM);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.Include(x => x.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Class")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IEnumerable<Driver> drivers = _context.Driver;
            ViewBag.Drivers = new SelectList(drivers, "Id", "FirstName"/*, "LastName", "PhoneNumber"*/);

            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Class,DriverId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.Include(x => x.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'TaxiContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
