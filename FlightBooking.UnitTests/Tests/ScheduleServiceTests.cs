using System.Linq;
using FlightBooking.Core.Models;
using FlightBooking.Core.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlightBooking.UnitTests.Tests
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private AppSettings createAppSettings()
        {
            return new AppSettings
            {
                RelaxRequiremnts = false,
                FlightRoute = new FlightRoute("London", "Paris")
                {
                    BaseCost = 50,
                    BasePrice = 100,
                    LoyaltyPointsGained = 5,
                    MinimumTakeOffPercentage = 0.7
                },
                Aircrafts = new Plane[]
                {
                    new Plane
                    {
                        Id = 123,
                        Name = "Ahmed 101",
                        NumberOfSeats = 1
                    },
                    new Plane
                    {
                        Id = 1,
                        Name = "Airlines 101",
                        NumberOfSeats = 20
                    }
                }
            };
        }

        [TestMethod]
        public void ScheduleService_AddPassenger_AddsToProperty()
        {
            var firstPassenger = new Passenger
            {
                Type = PassengerType.General,
                Name = "Ahmed",
                Age = 24,
                AllowedBags = 1
            };

            var secondPassenger = new Passenger
            {
                Type = PassengerType.LoyaltyMember,
                Name = "John",
                Age = 22,
                AllowedBags = 2
            };

            var scheduleService = new ScheduleService(Options.Create(createAppSettings()));
            scheduleService.AddPassenger(firstPassenger);
            scheduleService.AddPassenger(secondPassenger);

            Assert.AreEqual(2, scheduleService.Passengers.Count);
            Assert.AreEqual(firstPassenger.Type, scheduleService.Passengers.First().Type);
            Assert.AreEqual(firstPassenger.Name, scheduleService.Passengers.First().Name);
        }

        [TestMethod]
        public void ScheduleService_GetSummary_FlightProceeded()
        {
            var firstPassenger = new Passenger
            {
                Type = PassengerType.General,
                Name = "Ahmed",
                Age= 24,
                AllowedBags = 1
            };
            
            var scheduleService = new ScheduleService(Options.Create(createAppSettings()));
            scheduleService.AddPassenger(firstPassenger);

            var result = scheduleService.GetSummary();
            Assert.IsTrue(result.Contains("FLIGHT MAY PROCEED"));
        }
        
        [TestMethod]
        public void ScheduleService_GetSummary_FlightNotProceed_ShowsOtherFlights()
        {
            var firstPassenger = new Passenger
            {
                Type = PassengerType.General,
                Name = "Ahmed",
                Age= 24,
                AllowedBags = 1
            };
            
            var secondPassenger = new Passenger
            {
                Type = PassengerType.LoyaltyMember,
                Name = "John",
                Age= 22,
                AllowedBags = 2
            };
            
            var scheduleService = new ScheduleService(Options.Create(createAppSettings()));
            scheduleService.AddPassenger(firstPassenger);
            scheduleService.AddPassenger(secondPassenger);

            var result = scheduleService.GetSummary();
            Assert.IsTrue(result.Contains("Airlines 101"));
        }
    }
}