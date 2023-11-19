namespace TaxiMVC.Models.Car
{
    public class SortCarVM
    {
        public CarsSortState ClassSort { get; set; }
        public CarsSortState DriverSort { get; set; }
        public CarsSortState Current { get; set; }
        public bool Up { get; set; }

        public SortCarVM(CarsSortState sortState)
        {
            ClassSort = CarsSortState.ClassAsc;
            DriverSort = CarsSortState.DriverAsc;
            Up = true;

            if (sortState == CarsSortState.ClassDesc ||
                sortState == CarsSortState.DriverDesc)
            {
                Up = false;
            }

            Current = sortState switch
            {
                CarsSortState.ClassDesc => Current = ClassSort = CarsSortState.ClassAsc,
                CarsSortState.DriverAsc => Current = DriverSort = CarsSortState.DriverDesc,
                CarsSortState.DriverDesc => Current = DriverSort = CarsSortState.DriverAsc,
                _ => Current = ClassSort = CarsSortState.ClassDesc
            };
        }
    }
}
