using System;
using AirConsole.Solution.Commands;
using AirConsole.Solution.Data;
using AirConsole.Solution.Screens;
using Microsoft.Extensions.DependencyInjection;

namespace AirConsole.Solution
{
    static class Program
    {
        static void Main()
        {
            var serviceProvider = new ServiceCollection() //??
                .AddSingleton<MainScreen>()
                .AddSingleton<SeatClassSelectionScreen>()
                .AddSingleton<BusinessClassSeatSelection>()
                .AddSingleton<EconomyClassSeatSelection>()
                .AddSingleton<SeatRepository>()
                .AddSingleton(sp => sp)
                .BuildServiceProvider();

            ICommand currentScreen = serviceProvider.GetService<MainScreen>();
            do
            {
                currentScreen = currentScreen?.Execute();
            } while (currentScreen != null);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}