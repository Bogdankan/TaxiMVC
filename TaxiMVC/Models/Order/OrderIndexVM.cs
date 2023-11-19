namespace TaxiMVC.Models.Order
{
    public class OrderIndexVM
    {
        public decimal Price { get; set; }
        public string? TripBegin { get; set; }
        public string? TripEnd { get; set; }
        public Car.Car? Car { get; set; }
        public DateTime OrderDateTime { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public SortOrderVM SortOrderVM { get; set; } = new SortOrderVM(OrdersSortState.PriceAsc);
        public FilterOrderVM FilterOrderVM { get; set; }
        public PageVM PageVM { get; set; }
    }
}
