using System;
using FlightBooking.Core.Services;

namespace FlightBooking.Core.Views
{
    public class ConsoleView : IConsoleView
    {
        private IScheduleService _scheduleService;
        public ConsoleView(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public void PrintSummary()
        {
            System.Console.WriteLine();
            System.Console.WriteLine(_scheduleService.GetSummary());
        }

        public void PrintError()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("UNKNOWN INPUT");
            System.Console.ResetColor();
        }

        public void PrintSuccessfullyAddedPassenger(string passengerType, string passengerName)
        {
            System.Console.WriteLine($"{passengerType} passenger {passengerName} has successfully been added");
            System.Console.WriteLine();
        }

        
        
        public string GetUserInput()
        {
            System.Console.WriteLine("Please enter command."); 
            return System.Console.ReadLine() ?? "";
        }
    }
}