using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxiMVC.Models.Car
{
    public class FilterCarVM
    {
        public string? Class { get; set; }        
        public int? DriverId { get; set; }
        public SelectList Drivers { get; set; } = new SelectList(new List<Models.Driver.Driver>(), "Id", "FirstName");
    }
}
