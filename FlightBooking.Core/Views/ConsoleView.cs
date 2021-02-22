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

        public void PrintProgrammeStarted()
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Starting Programme! Here are a list of known commands:");
            knownCommands();
            System.Console.WriteLine();
            System.Console.ResetColor();
        }

        public void PrintSummary()
        {
            System.Console.WriteLine();
            System.Console.WriteLine(_scheduleService.GetSummary());
        }

        public void PrintError()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            System.Console.WriteLine("Unknown Command! Here are a list of known commands:");
            knownCommands();
            System.Console.WriteLine();
            System.Console.ResetColor();
        }

        public void PrintMissingFields()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine();
            System.Console.WriteLine("You are missing fields! Here are a list of known commands with their correct fields:");
            knownCommands();
            System.Console.WriteLine();
            System.Console.ResetColor();
        }

        public void PrintExiting()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Exiting.....");
        }

        public void PrintSuccessfullyAddedPassenger(string passengerType, string passengerName)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{passengerType} passenger {passengerName} has successfully been added");
            System.Console.WriteLine();
            System.Console.ResetColor();
        }
        
        public string GetUserInput()
        {
            System.Console.WriteLine("Please enter command."); 
            return System.Console.ReadLine() ?? "";
        }
        
        /// <summary>
        /// This method is used to give users an indication of what all the possible commands are.
        /// This prints a list of all the valid commands and its structure
        /// </summary>
        private void knownCommands()
        {
            System.Console.WriteLine("---- Add Passenger (General/Discounted/Airline Employee) ----");
            System.Console.WriteLine("<add> <general/discounted/airline> <insert name> <insert age>");
            System.Console.WriteLine();
            System.Console.WriteLine("---- Add Loyalty Passenger ----");
            System.Console.WriteLine("<add> <loyalty> <insert name> <insert age> <insert loyalty points> <true/false>(Is passenger using loyalty points)");
            System.Console.WriteLine();
            System.Console.WriteLine("---- Print Summary ----");
            System.Console.WriteLine("<print> <summary>");
            System.Console.WriteLine();
            System.Console.WriteLine("---- Exit Programme ----");
            System.Console.WriteLine("<exit>");
        }
    }
}