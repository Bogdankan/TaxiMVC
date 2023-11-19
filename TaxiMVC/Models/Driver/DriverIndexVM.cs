namespace TaxiMVC.Models.Driver
{
    public class DriverIndexVM
    {
        public Driver Driver { get; set; }

        public IEnumerable<Driver> Drivers { get; set; } = new List<Driver>();
        public SortDriverVM SortDriverVM { get; set; } = new SortDriverVM(DriversSortState.FirstNameAsc);
        public FilterDriverVM FilterDriverVM { get; set; }
        public PageVM PageVM { get; set; }
    }
}
