namespace FlightBooking.Core.Models
{
    public class AppSettings
    {
        public bool RelaxRequiremnts { get; set; }
        public FlightRoute FlightRoute { get; set; }
        public Plane[] Aircrafts { get; set; }
    }
}