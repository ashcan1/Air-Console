using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTables;
using AirConsole.Solution.Commands;
using AirConsole.Solution.Data;
using AirConsole.Solution.Models;

namespace AirConsole.Solution.Screens
{
    public abstract class SeatSelectionBase : ScreenBase
    {
        public int StartRow { get; private set; }
        public int EndRow { get; private set; }

        private readonly MainScreen mainScreen;
        private readonly SeatRepository repository;
        public SeatSelectionBase(string seatClass, int startRow, int endRow, IServiceProvider serviceProvider): 
            base(
                seatClass, 
                null
            )
        {
            StartRow = startRow;
            EndRow = endRow;
            mainScreen = serviceProvider.GetService<MainScreen>();
            repository = serviceProvider.GetService<SeatRepository>();
            
            var seatLetters = new char[]{ 'A', 'B', 'C', 'D', 'E' };
            for (int rowNumber = StartRow; rowNumber <= EndRow; rowNumber++)
            {
                foreach (var seatLetter in seatLetters)
                {
                    var seatId = $"{rowNumber}{seatLetter}";
                    if (repository.Get(seatId) != null)
                    {
                        continue;
                    }
                    
                    var seat = new Seat(seatId);
                    repository.Add(seat);
                }
            }
        }
        
        public override ICommand Execute()
        {
            DrawHeader();
            
            var table = new ConsoleTable( " ", "A", "B", "C", "D", "E");
            table.Options.EnableCount = false;
            for (int rowNumber = StartRow; rowNumber <= EndRow; rowNumber++)
            {
                table.AddRow
                (
                    rowNumber, 
                    GetSeatReservationStatus($"{rowNumber}A"), 
                    GetSeatReservationStatus($"{rowNumber}B"), 
                    GetSeatReservationStatus($"{rowNumber}C"), 
                    GetSeatReservationStatus($"{rowNumber}D"), 
                    GetSeatReservationStatus($"{rowNumber}E")
                );
            }
            table.Write();

            var seatInput = SeatPrompt();
            seatInput.Passenger = new Person();
            
            Console.Write("Please enter your firstname: ");
            seatInput.Passenger.Firstname = Console.ReadLine();
            
            Console.Write("Please enter your lastname: ");
            seatInput.Passenger.Lastname = Console.ReadLine();
            
            Console.Write("Please enter your passport: ");
            seatInput.Passenger.Passport = Console.ReadLine();
            repository.Edit(seatInput);

            Console.WriteLine();
            Console.WriteLine("Passenger details saved!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            return mainScreen;
        }

        private Seat SeatPrompt()
        {
            Seat seat = null;
            do
            {
                var rowNumberInput = Prompt
                (
                    "Please enter the row number",
                    (char input) =>
                    {
                        var numericValue = Char.GetNumericValue(input); 
                        return numericValue >= StartRow && numericValue <= EndRow;
                    }
                );
            
                var seatLetterInput = Prompt
                (
                    "Please enter the seat letter",
                    (char input) =>
                    {
                        var seatLetters = new char[]{ 'A', 'B', 'C', 'D', 'E' }.ToList();
                        return seatLetters.IndexOf(input) != -1;
                    }
                );
            
                var seatId = $"{rowNumberInput}{seatLetterInput}";

                if (!repository.IsAvailable(seatId))
                {
                    Console.WriteLine("Seat is taken! Please try again.");
                    Console.WriteLine();
                }
                else
                {
                    seat = repository.Get(seatId);
                }
            } while (seat ==  null);

            return seat;
        }
        
        private string GetSeatReservationStatus(string seatNumber)
        {
            return repository.IsAvailable(seatNumber) ? " " : "X";
        }

        private char Prompt(string message, Func<char, bool> validate)
        {
            bool isValid = false;
            char returnValue;
            do
            {
                Console.Write($"{message}: ");
                returnValue = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
                
                isValid = validate(returnValue); 
                if (!isValid)
                {
                    Console.WriteLine("Invalid Entry! Please try again.");
                    continue;
                }
            } while (!isValid);

            return returnValue;
        }
    }
}