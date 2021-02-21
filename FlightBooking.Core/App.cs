using FlightBooking.Core.Services;

namespace FlightBooking.Core
{
    public class App
    {
        private readonly IFlightService _flightService;
        public App(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public void Run()
        {
            _flightService.Run();
        }
    }
}