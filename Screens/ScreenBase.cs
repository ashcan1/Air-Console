using System;
using AirConsole.Solution.Commands;
namespace AirConsole.Solution.Screens
{
    public abstract class ScreenBase : ICommand
    {
        public Menu.Menu Menu { get; private set; }
        public string Title { get; private set; }

        public ScreenBase(string title, Menu.Menu menu)
        {
            Title = title;
            Menu = menu;
        }

        protected void DrawHeader()
        {
            Console.WriteLine(new String('*', Title.Length + 8));
            Console.WriteLine($"**  {Title}  **");
            Console.WriteLine(new String('*', Title.Length + 8));
            Console.WriteLine();
        }
        
        public virtual ICommand Execute()
        {
            DrawHeader();

            return Menu?.Execute();
        }
    }
}