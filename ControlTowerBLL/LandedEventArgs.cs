using System;

namespace ControlTowerBLL
{
    public class LandedEventArgs : EventArgs
    {
        public Flight Flight { get; }

        public LandedEventArgs(Flight flight)
        {
            Flight = flight;
        }
    }
}
