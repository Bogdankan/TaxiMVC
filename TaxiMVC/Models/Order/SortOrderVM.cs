namespace TaxiMVC.Models.Order
{
    public class SortOrderVM
    {
        public OrdersSortState PriceSort { get; set; }
        public OrdersSortState TripBeginSort { get; set; }
        public OrdersSortState TripEndSort { get; set; }
        public OrdersSortState CarSort { get; set; }
        public OrdersSortState OrderDateTimeSort { get; set; }
        public OrdersSortState Current { get; set; }
        public bool Up { get; set; }

        public SortOrderVM(OrdersSortState sortState)
        {
            PriceSort = OrdersSortState.PriceAsc;
            TripBeginSort = OrdersSortState.TripBeginAsc;
            TripEndSort = OrdersSortState.TripEndAsc;
            CarSort = OrdersSortState.CarAsc;
            OrderDateTimeSort = OrdersSortState.OrderDateTimeAsc;
            Up = true;

            if (sortState == OrdersSortState.PriceDesc ||
                sortState == OrdersSortState.TripBeginDesc ||
                sortState == OrdersSortState.TripEndDesc ||
                sortState == OrdersSortState.CarDesc ||
                sortState == OrdersSortState.OrderDateTimeDesc)
            {
                Up = false;
            }

            Current = sortState switch
            {
                OrdersSortState.PriceDesc => Current = PriceSort = OrdersSortState.PriceAsc,
                OrdersSortState.TripBeginAsc => Current = TripBeginSort = OrdersSortState.TripBeginDesc,
                OrdersSortState.TripBeginDesc => Current = TripBeginSort = OrdersSortState.TripBeginAsc,
                OrdersSortState.TripEndAsc => Current = TripEndSort = OrdersSortState.TripEndDesc,
                OrdersSortState.TripEndDesc => Current = TripEndSort = OrdersSortState.TripEndAsc,
                OrdersSortState.CarAsc => Current = CarSort = OrdersSortState.CarDesc,
                OrdersSortState.CarDesc => Current = CarSort = OrdersSortState.CarAsc,
                OrdersSortState.OrderDateTimeAsc => Current = OrderDateTimeSort = OrdersSortState.OrderDateTimeDesc,
                OrdersSortState.OrderDateTimeDesc => Current = OrderDateTimeSort = OrdersSortState.OrderDateTimeAsc,
                _ => Current = PriceSort = OrdersSortState.PriceDesc
            };
        }
    }
}
