using Microsoft.AspNetCore.Mvc;

namespace TaxiMVC.Models.Driver
{
    public class DriverListVM
    {
        public IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
