﻿namespace FlightBooking.Core.Views
{
    public interface IConsoleView
    {
        void PrintSummary();
        void PrintError();
        void PrintExiting();
        void PrintSuccessfullyAddedPassenger(string passangeType, string passengerName);
        string GetUserInput();
    }
}