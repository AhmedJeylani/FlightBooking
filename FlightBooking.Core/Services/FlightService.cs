using System;
using FlightBooking.Core.Models;
using FlightBooking.Core.Views;

namespace FlightBooking.Core.Services
{
    public class FlightService : IFlightService
    {
        private readonly IScheduleService _scheduleService;
        private readonly IConsoleView _consoleView;
        public FlightService(IScheduleService scheduleService, IConsoleView consoleView)
        {
            _scheduleService = scheduleService;
            _consoleView = consoleView;
        }

        public void Run()
        {
            var keepRunning = true;
            do
            {
                var command = _consoleView.GetUserInput();
                var enteredText = command.ToLower();
                if (enteredText.Contains("print summary"))
                {
                    _consoleView.PrintSummary();
                }
                else if (enteredText.Contains("add general"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduleService.AddPassenger(new Passenger
                    {
                        Type = PassengerType.General, 
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                        AllowedBags = 1
                    });
                    _consoleView.PrintSuccessfullyAddedPassenger("General", passengerSegments[2]);
                }
                else if (enteredText.Contains("add loyalty"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduleService.AddPassenger(new Passenger
                    {
                        Type = PassengerType.LoyaltyMember, 
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                        AllowedBags = 2,
                        LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
                        IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
                    });
                    _consoleView.PrintSuccessfullyAddedPassenger("Loyalty", passengerSegments[2]);
                }
                else if (enteredText.Contains("add discounted"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduleService.AddPassenger(new Passenger
                    {
                        Type = PassengerType.Discounted,
                        Name = passengerSegments[2],
                        Age = Convert.ToInt32(passengerSegments[3]),
                        AllowedBags = 0,
                    });
                    _consoleView.PrintSuccessfullyAddedPassenger("Discounted", passengerSegments[2]);
                }
                else if (enteredText.Contains("add airline"))
                {
                    var passengerSegments = enteredText.Split(' ');
                    _scheduleService.AddPassenger(new Passenger
                    {
                        Type = PassengerType.AirlineEmployee, 
                        Name = passengerSegments[2], 
                        Age = Convert.ToInt32(passengerSegments[3]),
                        AllowedBags = 1,
                    });
                    _consoleView.PrintSuccessfullyAddedPassenger("Airline Employee", passengerSegments[2]);
                }
                else if (enteredText.Contains("exit"))
                {
                    keepRunning = false;
                    _consoleView.PrintExiting();
                }
                else
                {
                    _consoleView.PrintError();
                }
            } while (keepRunning);
        }
    }
}