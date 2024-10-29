using ControlTowerBLL;
using ControlTowerDTO;

namespace ControlTowerServices
{
    /// <summary>
    /// Service class for managing flights. It provides functionalities to add, take off, land, and change the altitude of flights.
    /// </summary>
    public class FlightService
    {
        private ControlTower controlTower;

        /// <summary>
        /// Initializes a new instance of the FlightService class, setting up the ControlTower.
        /// </summary>
        public FlightService() => controlTower = new ControlTower();

        /// <summary>
        /// Adds a flight to the control tower's flight list.
        /// </summary>
        /// <param name="flightDTO">The flight data to add.</param>
        public void AddFlight(FlightDTO flightDTO)
        {
            Flight flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration);
            controlTower.AddFlight(flight);
        }
        /// <summary>
        /// Initiates the takeoff process for a flight.
        /// </summary>
        /// <param name="flightDTO">The flight data representing the flight to take off.</param>
        public void TakeOffFlight(FlightDTO flightDTO)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.TakeOffFlight(flight);
        }
        /// <summary>
        /// Lands a flight.
        /// </summary>
        /// <param name="flightDTO">The flight data representing the flight to land.</param>
        public void LandFlight(FlightDTO flightDTO)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.LandFlight(flight);
        }
        /// <summary>
        /// Changes the flight's altitude by a given value.
        /// </summary>
        /// <param name="flightDTO">The flight data representing the flight whose altitude is to be changed.</param>
        /// <param name="newHeight">The value by which to change the flight's altitude.</param>
        public void ChangeFlightHeight(FlightDTO flightDTO, int newHeight)
        {
            Flight flight = controlTower.FindFlightById(flightDTO.Id);
            controlTower.ChangeFlightHeight(flight, newHeight);
        }
        /// <summary>
        /// Subscribes to the takeoff event for flights.
        /// </summary>
        /// <param name="handler">The event handler to be triggered when a flight takes off.</param>
        public void SubscribeToTakeOff(EventHandler<TakeOffEventArgs> handler) =>
            controlTower.FlightTakeOff += handler;

        /// <summary>
        /// Subscribes to the landing event for flights.
        /// </summary>
        /// <param name="handler">The event handler to be triggered when a flight lands.</param>
        public void SubscribeToLanding(EventHandler<LandedEventArgs> handler) =>
            controlTower.FlightLanded += handler;
    }
}
