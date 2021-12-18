using System;
using AirConsole.Solution.Commands;

namespace AirConsole.Solution.Menu
{
    public class MenuItem
    {
        public string Shortcut { get; private set; }
        public string Label { get; private set; }
        public Lazy<ICommand> Command { get; private set; }

        public MenuItem(string shortcut, string label, Lazy<ICommand> command)
        {
            Shortcut = shortcut;
            Label = label;
            Command = command;
        }
    }
}