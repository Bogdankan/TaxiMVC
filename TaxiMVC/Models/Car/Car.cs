using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiMVC.Models.Driver;
namespace TaxiMVC.Models.Car
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string? Class { get; set; }
        public int DriverId { get; set; }
        public TaxiMVC.Models.Driver.Driver? Driver { get; set; }
    }
}

