using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxiMVC.Data;
using TaxiMVC.Models;
using TaxiMVC.Models.Car;
using TaxiMVC.Models.Order;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiMVC.Controllers
{
    //[Authorize]
    public class OrdersController : Controller
    {
        private readonly TaxiContext _context;
         
        public OrdersController(TaxiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? tripBegin = null, string? tripEnd = null, int? car = null, OrdersSortState sortOrder = OrdersSortState.PriceAsc, int page = 1)
        {
            int pageSize = 3;
            IQueryable<Order>? orders = _context.Order.Include(x => x.Car);
            var count = await orders.CountAsync();
            orders = orders.Skip((page - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(tripBegin))
            {
                orders = orders.Where(x => x.TripBegin.Contains(tripBegin));
            }
            if (!string.IsNullOrEmpty(tripEnd))
            {
                orders = orders.Where(x => x.TripBegin.Contains(tripEnd));
            }
            if (car != null && car != 0)
            {
                orders = orders.Where(x => x.CarId == car);
            }

            List<Car> cars = await _context.Car.ToListAsync();
            cars.Insert(0, new Car { Class = "Всі", Id = 0 });

            ViewData["PriceSort"] = sortOrder == OrdersSortState.PriceAsc ? OrdersSortState.PriceDesc : OrdersSortState.PriceAsc;
            ViewData["TripBeginSort"] = sortOrder == OrdersSortState.TripBeginAsc ? OrdersSortState.TripBeginDesc : OrdersSortState.TripBeginAsc;
            ViewData["TripEndSort"] = sortOrder == OrdersSortState.TripEndAsc ? OrdersSortState.TripEndDesc : OrdersSortState.TripEndAsc;
            ViewData["CarSort"] = sortOrder == OrdersSortState.CarAsc ? OrdersSortState.CarDesc : OrdersSortState.CarAsc;
            ViewData["OrderDateTimeSort"] = sortOrder == OrdersSortState.OrderDateTimeAsc ? OrdersSortState.OrderDateTimeDesc : OrdersSortState.OrderDateTimeAsc;

            orders = sortOrder switch
            {
                OrdersSortState.PriceDesc => orders.OrderByDescending(x => x.Price),
                OrdersSortState.TripBeginAsc => orders.OrderBy(x => x.TripBegin),
                OrdersSortState.TripBeginDesc => orders.OrderByDescending(x => x.TripBegin),
                OrdersSortState.TripEndAsc => orders.OrderBy(x => x.TripEnd),
                OrdersSortState.TripEndDesc => orders.OrderByDescending(x => x.TripEnd),
                OrdersSortState.CarAsc => orders.OrderBy(x => x.Car!.Class),
                OrdersSortState.CarDesc => orders.OrderByDescending(x => x.Car!.Class),
                OrdersSortState.OrderDateTimeAsc => orders.OrderBy(x => x.OrderDateTime),
                OrdersSortState.OrderDateTimeDesc => orders.OrderByDescending(x => x.OrderDateTime),
                _ => orders.OrderBy(x => x.Price)
            };

            PageVM pageVM = new PageVM(count, page, pageSize);

            FilterOrderVM filterOrderVM = new FilterOrderVM
            {
                TripBegin = tripBegin,
                TripEnd = tripEnd,
                CarId = car,
                Cars = new SelectList(cars, "Id", "Class", car)
            };

            SortOrderVM sortOrderVM = new SortOrderVM(sortOrder);

            OrderIndexVM orderIndexVM = new OrderIndexVM
            {
                Orders = await orders.AsNoTracking().ToListAsync(),
                FilterOrderVM = filterOrderVM,
                SortOrderVM = sortOrderVM,
                PageVM = pageVM,
            };


            return View(orderIndexVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(x => x.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            IEnumerable<Car> cars = _context.Car;
            ViewBag.Cars = new SelectList(cars, "Id", "Class", "Driver");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,TripBegin,TripEnd,OrderDateTime, CarId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            IEnumerable<Car> cars = _context.Car;
            ViewBag.Cars = new SelectList(cars, "Id", "Class", "Driver");

            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(x => x.Car).FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,TripBegin,TripEnd,OrderDateTime, CarId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(x => x.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'TaxiContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
