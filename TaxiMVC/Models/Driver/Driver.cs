using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaxiMVC.Models.Driver
{
    public class Driver
    {
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }
}