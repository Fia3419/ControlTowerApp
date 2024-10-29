using System;
using ControlTowerBLL.Managers;
using ControlTowerDTO;

namespace ControlTowerBLL
{
    public class ControlTower : ListManager<Flight>
    {
        public event EventHandler<TakeOffEventArgs> FlightTakeOff;
        public event EventHandler<LandedEventArgs> FlightLanded;

        public delegate int ChangeAltitudeDelegate(int currentAltitude, int changeValue);
        public ChangeAltitudeDelegate ChangeAltitudeHandler { get; private set; }

        public ControlTower()
        {
            ChangeAltitudeHandler = ChangeAltitude;
        }

        private int ChangeAltitude(int currentAltitude, int changeValue)
        {
            return currentAltitude + changeValue;
        }

        public void AddFlight(Flight flight)
        {
            flight.FlightTakeOff += OnFlightTakeOff;
            flight.FlightLanded += OnFlightLanded;
            Add(flight);
        }

        public Flight FindFlightById(string flightId)
        {
            return items.Find(f => f.FlightData.Id == flightId);
        }

        public void TakeOffFlight(Flight flight)
        {
            if (flight != null && !flight.FlightData.InFlight)
            {
                flight.TakeOffFlight();
            }
        }

        public void ChangeFlightHeight(Flight flight, int changeValue)
        {
            if (flight != null && flight.FlightData.InFlight)
            {
                int newAltitude = ChangeAltitudeHandler(flight.FlightHeight, changeValue);
                flight.ChangeFlightHeight(newAltitude);
            }
        }

        public void LandFlight(Flight flight)
        {
            if (flight != null)
            {
                flight.LandFlight();
                FlightLanded?.Invoke(this, new LandedEventArgs(flight));
            }
        }

        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            FlightTakeOff?.Invoke(this, e);
        }

        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            FlightLanded?.Invoke(this, e);
        }
    }
}