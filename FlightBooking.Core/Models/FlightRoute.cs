namespace FlightBooking.Core.Models
{
    public class FlightRoute
    {
        // This was added for the appsettings json file to set the properties correctly
        // I haven't changed how this code works but I have added things that supports this programme
        public FlightRoute()
        {
        }

        public FlightRoute(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
        }
        
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Title => Origin + " to " + Destination;
        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }
    }
}
