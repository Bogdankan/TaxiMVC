namespace TaxiMVC.Models.Driver
{
    public class SortDriverVM
    {
        public DriversSortState FirstNameSort { get; set; }
        public DriversSortState LastNameSort { get; set; }
        public DriversSortState Current { get; set; }
        public bool Up { get; set; }

        public SortDriverVM(DriversSortState sortState)
        {
            FirstNameSort = DriversSortState.FirstNameAsc;
            LastNameSort = DriversSortState.SecondNameAsc;
            Up = true;

            if (sortState == DriversSortState.FirstNameDesc ||
                sortState == DriversSortState.SecondNameDesc)
            {
                Up = false;
            }

            Current = sortState switch
            {
                DriversSortState.FirstNameDesc => Current = FirstNameSort = DriversSortState.FirstNameAsc,
                DriversSortState.SecondNameAsc => Current = LastNameSort = DriversSortState.SecondNameDesc,
                DriversSortState.SecondNameDesc => Current = LastNameSort = DriversSortState.SecondNameAsc,
                _ => Current = FirstNameSort = DriversSortState.FirstNameDesc
            };
        }
    }
}
