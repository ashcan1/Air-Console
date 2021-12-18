using System;

namespace AirConsole.Solution.Screens
{
    public class EconomyClassSeatSelection : SeatSelectionBase
    {
        public EconomyClassSeatSelection(IServiceProvider serviceProvider) 
            : base("Economy Class", 6, 8, serviceProvider)
        {
        }
    }
}