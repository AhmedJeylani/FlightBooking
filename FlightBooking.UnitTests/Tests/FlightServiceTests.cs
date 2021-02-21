using FlightBooking.Core;
using FlightBooking.Core.Services;
using FlightBooking.Core.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlightBooking.UnitTests.Tests
{
    [TestClass]
    public class FlightServiceTests
    {
	    [TestMethod]
		public void FlightService_GetUserInput_Called()
		{
			var mockConsoleView = new Mock<IConsoleView>();
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns("anything").Returns("exit");

			var flightService = new FlightService(Mock.Of<IScheduleService>(), mockConsoleView.Object);

			flightService.Run();

			mockConsoleView.Verify(x => x.GetUserInput(), Times.Once);
		}

		[TestMethod]
		public void FlightService_GetUserInput_IncorrectInput_PrintsError()
		{
			var mockConsoleView = new Mock<IConsoleView>();
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns("incorrect input").Returns("exit");

			var flightService = new FlightService(Mock.Of<IScheduleService>(), mockConsoleView.Object);
			
			flightService.Run();
			
			mockConsoleView.Verify(x => x.PrintError(), Times.Once);
		}

		[TestMethod]
		public void FlightService_GetUserInput_PrintsSummary() 
		{ 
			var mockConsoleView = new Mock<IConsoleView>();
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns("print summary").Returns("exit");

			var flightService = new FlightService(Mock.Of<IScheduleService>(), mockConsoleView.Object);
			
			flightService.Run();
			
			mockConsoleView.Verify(x => x.PrintSummary(), Times.Once);
		}
		
		[TestMethod]
		public void FlightService_GetUserInput_PrintsExiting() 
		{ 
			var mockConsoleView = new Mock<IConsoleView>();
			mockConsoleView.Setup(x => x.GetUserInput()).Returns("exit");

			var flightService = new FlightService(Mock.Of<IScheduleService>(), mockConsoleView.Object);
			
			flightService.Run();
			
			mockConsoleView.Verify(x => x.PrintExiting(), Times.Once);
		}
		
		[DataRow("add general Steve 30", "General", "steve")]
		[DataRow("add loyalty John 29 1000 true", "Loyalty", "john")]
		[DataRow("add airline Trevor 47", "Airline Employee", "trevor")]
		[DataRow("add discounted Ahmed 30", "Discounted", "ahmed")]
		[TestMethod]
		public void FlightService_GetUserInput_AddPassenger_PrintSuccessfullyAddedPassenger(string userInput, string passengerType, string passengerName)
		{ 
			var mockConsoleView = new Mock<IConsoleView>();
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns(userInput).Returns("exit");

			var flightService = new FlightService(Mock.Of<IScheduleService>(), mockConsoleView.Object);
			
			flightService.Run();
			
			mockConsoleView.Verify(x => x.PrintSuccessfullyAddedPassenger(It.Is<string>(p => p.Equals(passengerType)), It.Is<string>(p => p.Equals(passengerName))), Times.Once);
		}
		
		[DataRow("add general", PassengerType.General, "steve", 30, 1)]
		[DataRow("add airline", PassengerType.AirlineEmployee, "trevor", 47, 1)]
		[DataRow("add discounted", PassengerType.Discounted, "ahmed", 30, 0)]
		[TestMethod]
		public void FlightService_GetUserInput_AddPassenger(string userInput, PassengerType passengerType, string name, int age, int allowedBags)
		{ 
			var mockConsoleView = new Mock<IConsoleView>();
			var mockScheduleService = new Mock<IScheduleService>();
			
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns($"{userInput} {name} {age}").Returns("exit");


			var flightService = new FlightService(mockScheduleService.Object, mockConsoleView.Object);
			
			flightService.Run();
			
			mockScheduleService.Verify(x => x.AddPassenger(It.Is<Passenger>(
				p => p.Type == passengerType && p.Age == age && p.Name == name && p.AllowedBags == allowedBags
			)), Times.Once);
			}
		
		[TestMethod]
		public void FlightService_GetUserInput_AddPassenger_Loyalty()
		{ 
			var mockConsoleView = new Mock<IConsoleView>();
			var mockScheduleService = new Mock<IScheduleService>();
			var name = "john";
			var age = 32;
			var allowedBags = 2;
			
			mockConsoleView.SetupSequence(x => x.GetUserInput()).Returns($"add loyalty {name} {age} 1000 true").Returns("exit");
			
			var flightService = new FlightService(mockScheduleService.Object, mockConsoleView.Object);
			
			flightService.Run();
			
			mockScheduleService.Verify(x => x.AddPassenger(It.Is<Passenger>(
				p => p.Type == PassengerType.LoyaltyMember && p.Age == age && p.Name == name && p.AllowedBags == allowedBags && p.IsUsingLoyaltyPoints && p.LoyaltyPoints == 1000
			)), Times.Once);
		}

	}
}
