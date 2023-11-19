using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using TaxiMVC.Models.Car;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiMVC.Models.Order
{
    public class FilterOrderVM
    {
        public string? TripBegin { get; set; }
        public string? TripEnd { get; set; }
        public int? CarId { get; set; }
        public SelectList Cars { get; set; } = new SelectList(new List<Models.Car.Car>(), "Id", "Class");
    }
}
