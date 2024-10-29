using ControlTowerBLL;
using ControlTowerDTO;

namespace ControlTowerServices
{
    public class FlightService
    {
        private ControlTower controlTower;

        public FlightService()
        {
            controlTower = new ControlTower();
        }

        public void AddFlight(FlightDTO flightDTO)
        {
            Flight flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration);
            controlTower.AddFlight(flight);
        }

        public void TakeOffFlight(FlightDTO flightDTO)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.TakeOffFlight(flight);
        }

        public void LandFlight(FlightDTO flightDTO)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.LandFlight(flight);
        }

        public void ChangeFlightHeight(FlightDTO flightDTO, int newHeight)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.ChangeFlightHeight(flight, newHeight);
        }

        public void SubscribeToTakeOff(EventHandler<TakeOffEventArgs> handler)
        {
            controlTower.FlightTakeOff += handler;
        }

        public void SubscribeToLanding(EventHandler<LandedEventArgs> handler)
        {
            controlTower.FlightLanded += handler;
        }
    }
}
