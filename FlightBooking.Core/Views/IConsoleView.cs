namespace FlightBooking.Core.Views
{
    public interface IConsoleView
    {
        void PrintProgrammeStarted();
        void PrintSummary();
        void PrintError();
        void PrintMissingFields();
        void PrintExiting();
        void PrintSuccessfullyAddedPassenger(string passangeType, string passengerName);
        string GetUserInput();
    }
}