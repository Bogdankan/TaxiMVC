namespace TaxiMVC.Models
{
    public enum DriversSortState
    {
        FirstNameAsc,
        FirstNameDesc,
        SecondNameAsc,
        SecondNameDesc,
    }

    public enum CarsSortState
    {
        ClassAsc,
        ClassDesc,
        DriverAsc,
        DriverDesc,
    }

    public enum OrdersSortState
    {
        PriceAsc,
        PriceDesc,
        TripBeginAsc,
        TripBeginDesc,
        TripEndAsc,
        TripEndDesc,
        CarAsc,
        CarDesc,
        OrderDateTimeAsc,
        OrderDateTimeDesc,
    }
}
