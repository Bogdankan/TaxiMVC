using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxiMVC.Models.Car;
using TaxiMVC.Models.Driver;
using TaxiMVC.Models.Order;

namespace TaxiMVC.Data
{
    public class TaxiContext : DbContext
    {
        public TaxiContext (DbContextOptions<TaxiContext> options)
            : base(options)
        {
        }

        public DbSet<Driver>? Driver { get; set; }

        public DbSet<Order>? Order { get; set; }

        public DbSet<Car>? Car { get; set; }
    }
}
