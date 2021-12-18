namespace AirConsole.Solution.Models
{
    public class Seat : Model<string>
    {
        public Seat(string seatNumber)
        {
            Id = seatNumber;
        }

        public Seat()
        {
            
        }
        
        public Person Passenger { get; set; }
    }
}