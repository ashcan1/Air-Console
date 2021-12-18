using System;
using AirConsole.Solution.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AirConsole.Solution.Screens
{
    public class MainScreen : ScreenBase
    {
        public MainScreen(IServiceProvider serviceProvider) : 
            base(
                "Welcome to AirConsole", 
                    new Menu.Menu(            
                        "Task Selection",
                        "Please enter the task you want to perform"
                    )
                )
        {
            Menu.AddMenuItem("R", "Reservation", new Lazy<ICommand>(serviceProvider.GetService<SeatClassSelectionScreen>));
            Menu.AddMenuItem("S", "Seat Verification", new Lazy<ICommand>(serviceProvider.GetService<SeatClassSelectionScreen>));
            Menu.AddMenuItem("X", "Exit the System", new Lazy<ICommand>(() => null));
        }
    }
}