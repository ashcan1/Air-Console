using System;
using System.Collections.Generic;
using AirConsole.Solution.Commands;

namespace AirConsole.Solution.Menu
{
    public class Menu : ICommand
    {
        public string Title { get; private set; }
        public string Prompt { get; private set; }
        public List<MenuItem> MenuItems { get; private set; }

        public Menu(string title, string prompt)
        {
            Title = title;
            Prompt = prompt;
            MenuItems = new List<MenuItem>();
        }

        public void AddMenuItem(string shortcut, string label, Lazy<ICommand> command)
        {
            MenuItems.Add(new MenuItem(shortcut, label, command));
        }
        
        public ICommand Execute()
        {
            if (MenuItems.Count == 0)
            {
                return null;
            }

            Console.WriteLine($"{Title}:");
            foreach (var menuItem in MenuItems)
            {
                Console.WriteLine($"{menuItem.Shortcut}: {menuItem.Label}");
            }
            Console.WriteLine();

            MenuItem selectedMenuItem = null;
            do
            {
                Console.Write($"{Prompt}: ");
                var input = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                selectedMenuItem = MenuItems.Find(menuItem => menuItem.Shortcut == input.ToString());
                if (selectedMenuItem == null)
                {
                    Console.WriteLine("Invalid Entry! Please try again.");
                }
                Console.WriteLine();
            } while (selectedMenuItem == null);

            return selectedMenuItem.Command.Value;
        }
    }
}