using System;
using System.IO;
using FlightBooking.Core;
using FlightBooking.Core.Services;
using FlightBooking.Core.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("Configuration"));

            // Register Services
            serviceCollection.AddSingleton<App>();
            serviceCollection.AddSingleton<IScheduleService, ScheduleService>();
            serviceCollection.AddSingleton<IFlightService, FlightService>();
            serviceCollection.AddSingleton<IConsoleView, ConsoleView>();
        }
    }
}
