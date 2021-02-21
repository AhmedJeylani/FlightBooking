namespace FlightBooking.Core
{
    public class FlightRouteInfo
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Title => Origin + " to " + Destination;
        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }
    }
}
