using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace FlightBooking.Core.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";
        
        private readonly FlightRouteInfo _flightRoute;
        private readonly AircraftInfo _aircraft;
        
        public ScheduleService(IOptions<AppSettings> appSettingsInfo)
        {
            _flightRoute = appSettingsInfo.Value.FlightRouteInfo;
            _aircraft = appSettingsInfo.Value.AircraftInfo;
            Passengers = new List<Passenger>();
        }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }
        
        public string GetSummary()
        {
            double costOfFlight = 0;
            double profitFromFlight = 0;
            var totalLoyaltyPointsAccrued = 0;
            var totalLoyaltyPointsRedeemed = 0;
            var totalExpectedBaggage = 0;
            var seatsTaken = 0;

            var result = "Flight summary for " + _flightRoute.Title;

            foreach (var passenger in Passengers)
            {
                switch (passenger.Type)
                {
                    case PassengerType.General:
                        profitFromFlight += _flightRoute.BasePrice;
                        totalExpectedBaggage += passenger.AllowedBags;
                        break;
                        
                    case PassengerType.LoyaltyMember:
                        if (passenger.IsUsingLoyaltyPoints)
                        {
                            var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(_flightRoute.BasePrice));
                            passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                            totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                        }
                        else
                        {
                            totalLoyaltyPointsAccrued += _flightRoute.LoyaltyPointsGained;
                            profitFromFlight += _flightRoute.BasePrice;                           
                        }
                        totalExpectedBaggage += passenger.AllowedBags;
                        break;
                       
                    case PassengerType.AirlineEmployee:
                        totalExpectedBaggage += passenger.AllowedBags;
                        break;
                    
                    case PassengerType.Discounted:
                        profitFromFlight += _flightRoute.BasePrice / 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                costOfFlight += _flightRoute.BaseCost;
                seatsTaken++;
            }

            result += _verticalWhiteSpace;
            
            result += "Total passengers: " + seatsTaken;
            result += _newLine;
            result += Indentation + "General sales: " + Passengers.Count(p => p.Type == PassengerType.General);
            result += _newLine;
            result += Indentation + "Loyalty member sales: " + Passengers.Count(p => p.Type == PassengerType.LoyaltyMember);
            result += _newLine;
            result += Indentation + "Discounted sales: " + Passengers.Count(p => p.Type == PassengerType.Discounted);
            result += _newLine;
            result += Indentation + "Airline employee comps: " + Passengers.Count(p => p.Type == PassengerType.AirlineEmployee);

            
            result += _verticalWhiteSpace;
            result += "Total expected baggage: " + totalExpectedBaggage;

            result += _verticalWhiteSpace;

            result += "Total revenue from flight: " + profitFromFlight;
            result += _newLine;
            result += "Total costs from flight: " + costOfFlight;
            result += _newLine;

            var profitSurplus = profitFromFlight - costOfFlight;

            result += (profitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + profitSurplus;

            result += _verticalWhiteSpace;

            result += "Total loyalty points given away: " + totalLoyaltyPointsAccrued + _newLine;
            result += "Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed + _newLine;

            result += _verticalWhiteSpace;

            if (_flightRoute.RelaxRequiremnts)
            {
                if (seatsTaken < _aircraft.NumberOfSeats &&
                    seatsTaken / (double) _aircraft.NumberOfSeats > _flightRoute.MinimumTakeOffPercentage)
                {
                    result += "THIS FLIGHT MAY PROCEED";
                }
                else
                {
                    result += "FLIGHT MAY NOT PROCEED";
                }
            }
            else
            {
                if (profitSurplus > 0 &&
                    seatsTaken < _aircraft.NumberOfSeats &&
                    seatsTaken / (double) _aircraft.NumberOfSeats > _flightRoute.MinimumTakeOffPercentage)
                {
                    result += "THIS FLIGHT MAY PROCEED";
                }
                else
                {
                    result += "FLIGHT MAY NOT PROCEED";
                }
            }

            return result;
        }
        
        public List<Passenger> Passengers { get; }
    }
}