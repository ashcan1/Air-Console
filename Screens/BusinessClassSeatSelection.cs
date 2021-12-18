using System;

namespace AirConsole.Solution.Screens
{
    public class BusinessClassSeatSelection : SeatSelectionBase
    {
        public BusinessClassSeatSelection(IServiceProvider serviceProvider) 
            : base("Business Class", 1, 5, serviceProvider)
        {
        }
    }
}