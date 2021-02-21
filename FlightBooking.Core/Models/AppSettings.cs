namespace FlightBooking.Core
{
    public class AppSettings
    {
        public bool RelaxRequiremnts { get; set; }
        public FlightRouteInfo FlightRouteInfo { get; set; }
        public AircraftInfo[] Aircrafts { get; set; }
    }
}