using System;
using System.Collections.Generic;
using ControlTowerDTO;
using ControlTowerBLL;

namespace ControlTowerServices
{
    public class FlightService
    {
        private ControlTower controlTower = new ControlTower();

        public void AddFlight(FlightDTO flight)
        {
            controlTower.AddFlight(flight);
        }

        public List<FlightDTO> GetFlights()
        {
            return controlTower.GetFlights();
        }

        public void TakeOffFlight(FlightDTO flight)
        {
            controlTower.TakeOffFlight(flight);
        }

        public void LandFlight(FlightDTO flight)
        {
            controlTower.LandFlight(flight);
        }

        public void SubscribeToTakeOff(EventHandler<FlightEventArgs> handler)
        {
            controlTower.FlightTakeOff += handler;
        }

        public void SubscribeToLanding(EventHandler<FlightEventArgs> handler)
        {
            controlTower.FlightLanded += handler;
        }
    }
}