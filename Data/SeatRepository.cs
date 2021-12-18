using AirConsole.Solution.Models;

namespace AirConsole.Solution.Data
{
    public class SeatRepository : JsonFileRepository<Seat, string>
    {
        public SeatRepository() : base("flight-manifest.json")
        {
        }
        
        public bool IsAvailable (string seatNumber)
        {
            var seat = Get(seatNumber);
            if (seat?.Passenger != null)
            {
                return false;
            }

            return true;
        }
    }
}