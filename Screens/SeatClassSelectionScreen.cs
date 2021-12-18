using System;
using AirConsole.Solution.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AirConsole.Solution.Screens
{
    public class SeatClassSelectionScreen : ScreenBase
    {
        public SeatClassSelectionScreen(IServiceProvider serviceProvider) : 
            base(
                "Seat Class", 
                new Menu.Menu(
                    "Seat Class Selection",
                    "Please enter the seat class you want to reserve"
                )
            )
        {
            Menu.AddMenuItem("B", "Business Class", new Lazy<ICommand>(serviceProvider.GetService<BusinessClassSeatSelection>));
            Menu.AddMenuItem("E", "Economy Class", new Lazy<ICommand>(serviceProvider.GetService<EconomyClassSeatSelection>));
            Menu.AddMenuItem("X", "Exit to Main Menu", new Lazy<ICommand>(serviceProvider.GetService<MainScreen>));
        }
    }
}