using System;

namespace ControlTowerBLL
{
    public class FlightHeightChangedEventArgs : EventArgs
    {
        public Flight Flight { get; }
        public int NewHeight { get; }

        public FlightHeightChangedEventArgs(Flight flight, int newHeight)
        {
            Flight = flight;
            NewHeight = newHeight;
        }
    }
}
