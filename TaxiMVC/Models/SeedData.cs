using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiMVC.Data;
using System;
using System.Linq;
using TaxiMVC.Models;
using TaxiMVC.Models.Driver;
using System.Net.NetworkInformation;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new TaxiContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<TaxiContext>>()))
        {
            // Look for any drivers.
            if (context.Driver.Any() && context.Car.Any())
            {
                return;
            }
            context.Driver.AddRange(
                new Driver
                {
                    FirstName = "Ryan",
                    LastName = "Gosling",
                    PhoneNumber = "+380-64-125-34-82"
                },
                new Driver
                {
                    FirstName = "Christopher",
                    LastName = "Neal",
                    PhoneNumber = "+380-73-523-75-23"
                },
                new Driver
                {
                    FirstName = "Emma",
                    LastName = "Scott",
                    PhoneNumber = "+380-73-523-75-23"
                },
                new Driver
                {
                    FirstName = "Hilary",
                    LastName = "Nicholson",
                    PhoneNumber = "+380-53-756-21-84"
                }
            );
            context.SaveChanges();

            context.Car.AddRange(
            new TaxiMVC.Models.Car.Car
            {
                Class = "Business",
                Driver = context.Driver.FirstOrDefault() as Driver,
            },
            new TaxiMVC.Models.Car.Car
            {
                Class = "Standart",
                Driver = context.Driver.Skip(1).FirstOrDefault() as Driver,
            },
            new TaxiMVC.Models.Car.Car
            {
                Class = "Comfort",
                Driver = context.Driver.Skip(1).FirstOrDefault() as Driver,
            },
            new TaxiMVC.Models.Car.Car
            {
                Class = "Business",
                Driver = context.Driver.Skip(2).FirstOrDefault() as Driver,
            },
            new TaxiMVC.Models.Car.Car
            {
                Class = "Standart",
                Driver = context.Driver.Skip(3).FirstOrDefault() as Driver,
            },
            new TaxiMVC.Models.Car.Car
            {
                Class = "Comfort",
                Driver = context.Driver.Skip(3).FirstOrDefault() as Driver,
            }
        );
            context.SaveChanges();
        }
    }
}
