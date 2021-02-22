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
            _consoleView.PrintProgrammeStarted();
            do
            {
                try
                {
                    /*
                     * I would like to add a better handling of this and instead give users the ability to choose th
                     * instead of having to manually enter it is. This would lead to less human error
                     */
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
                }
                catch(Exception ex) 
                {
                    // This handles when the users adds the correct commands but doesn't fill
                    // out the rest of the fields e.g. name and age
                    // Would like to add better error handling and logging throughout the application. 
                    // Maybe a local log file 
                    if (ex is IndexOutOfRangeException)
                    {
                        _consoleView.PrintMissingFields();
                    }
                    else 
                    { 
                        _consoleView.PrintError();
                    }
                }

            } while (keepRunning);
        }
    }
}