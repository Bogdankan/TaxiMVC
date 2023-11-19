namespace TaxiMVC.Models.Car
{
    public class CarIndexVM
    {
        public Car Car{ get; set; }

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
        public SortCarVM SortCarVM { get; set; } = new SortCarVM(CarsSortState.ClassAsc);
        public FilterCarVM FilterCarVM { get; set; }
        public PageVM PageVM { get; set; }
    }
}
