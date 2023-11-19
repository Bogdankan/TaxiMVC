using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiMVC.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? TripBegin { get; set; }
        public string? TripEnd { get; set; }
        public int CarId { get; set; }
        public TaxiMVC.Models.Car.Car? Car { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderDateTime { get; set; }
    }
}