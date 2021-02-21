using FlightBooking.Core.Models;

namespace FlightBooking.Core.Services
{
    public interface IScheduleService
    {
        void AddPassenger(Passenger passenger);
        string GetSummary();
    }
}