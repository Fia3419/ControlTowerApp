using System;

namespace ControlTowerBLL
{
    public class TakeOffEventArgs : EventArgs
    {
        public Flight Flight { get; }

        public TakeOffEventArgs(Flight flight)
        {
            Flight = flight;
        }
    }
}
