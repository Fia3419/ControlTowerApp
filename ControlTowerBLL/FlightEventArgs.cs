using System;
using ControlTowerDTO;

namespace ControlTowerBLL
{
    public class FlightEventArgs : EventArgs
    {
        public Flight Flight { get; }
        public FlightDTO FlightDTO { get; }

        public FlightEventArgs(Flight flight)
        {
            Flight = flight;
            FlightDTO = flight.FlightData;
        }

        public FlightEventArgs(FlightDTO flightDTO)
        {
            FlightDTO = flightDTO;
        }
    }
}