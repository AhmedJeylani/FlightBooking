using System;
using System.IO;
using FlightBooking.Core;
using FlightBooking.Core.Models;
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
        
        /// <summary>
        /// This configures all the required services and injects the dependencies
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // build configuration
            /*
             * Using appsetting jsons, I would implement feature that allows the company to import
             * csv files or even from a database that would read the flights and aircrafts available.
             */
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
