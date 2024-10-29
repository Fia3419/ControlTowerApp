using System;
using ControlTowerBLL.Managers;
using ControlTowerDTO;

namespace ControlTowerBLL
{
    /// <summary>
    /// Represents the control tower responsible for managing flights, including takeoff, landing, and altitude changes.
    /// This class triggers events when flights take off or land.
    /// </summary>
    public class ControlTower : ListManager<Flight>
    {
        /// <summary>
        /// Event triggered when a flight takes off.
        /// </summary>
        public event EventHandler<TakeOffEventArgs> FlightTakeOff;

        /// <summary>
        /// Event triggered when a flight lands.
        /// </summary>
        public event EventHandler<LandedEventArgs> FlightLanded;

        /// <summary>
        /// Delegate for changing the flight altitude.
        /// </summary>
        /// <param name="currentAltitude">The current altitude of the flight.</param>
        /// <param name="changeValue">The value to change the altitude by.</param>
        /// <returns>The new altitude after applying the change.</returns>
        public delegate int ChangeAltitudeDelegate(int currentAltitude, int changeValue);

        /// <summary>
        /// Delegate instance used to change altitude.
        /// </summary>
        public ChangeAltitudeDelegate ChangeAltitudeHandler { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ControlTower class.
        /// </summary>
        public ControlTower()
        {
            ChangeAltitudeHandler = ChangeAltitude;
        }

        /// <summary>
        /// Changes the flight's altitude by a specific value.
        /// </summary>
        /// <param name="currentAltitude">The current altitude of the flight.</param>
        /// <param name="changeValue">The value to adjust the altitude by.</param>
        /// <returns>The new altitude after the change.</returns>
        private int ChangeAltitude(int currentAltitude, int changeValue)
        {
            return currentAltitude + changeValue;
        }

        /// <summary>
        /// Adds a new flight to the control tower and subscribes to its takeoff and landing events.
        /// </summary>
        /// <param name="flight">The flight to be added.</param>
        public void AddFlight(Flight flight)
        {
            flight.FlightTakeOff += OnFlightTakeOff;
            flight.FlightLanded += OnFlightLanded;
            Add(flight);
        }

        /// <summary>
        /// Finds a flight by its ID.
        /// </summary>
        /// <param name="flightId">The ID of the flight to find.</param>
        /// <returns>The flight with the specified ID, or null if not found.</returns>
        public Flight FindFlightById(string flightId)
        {
            return items.Find(f => f.FlightData.Id == flightId);
        }

        /// <summary>
        /// Initiates the takeoff procedure for a flight.
        /// </summary>
        /// <param name="flight">The flight to take off.</param>
        public void TakeOffFlight(Flight flight)
        {
            if (flight != null && !flight.FlightData.InFlight)
            {
                flight.TakeOffFlight();
            }
        }

        /// <summary>
        /// Changes the altitude of a flight while in the air.
        /// </summary>
        /// <param name="flight">The flight to change altitude for.</param>
        /// <param name="changeValue">The value by which to change the altitude.</param>
        public void ChangeFlightHeight(Flight flight, int changeValue)
        {
            if (flight != null && flight.FlightData.InFlight)
            {
                int newAltitude = ChangeAltitudeHandler(flight.FlightHeight, changeValue);
                flight.ChangeFlightHeight(newAltitude);
            }
        }

        /// <summary>
        /// Lands a flight and triggers the FlightLanded event.
        /// </summary>
        /// <param name="flight">The flight to land.</param>
        public void LandFlight(Flight flight)
        {
            if (flight != null)
            {
                flight.LandFlight();
                FlightLanded?.Invoke(this, new LandedEventArgs(flight));
            }
        }

        /// <summary>
        /// Event handler for when a flight takes off.
        /// </summary>
        /// <param name="sender">The flight that took off.</param>
        /// <param name="e">Event arguments containing the flight information.</param>
        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            FlightTakeOff?.Invoke(this, e);
        }

        /// <summary>
        /// Event handler for when a flight lands.
        /// </summary>
        /// <param name="sender">The flight that landed.</param>
        /// <param name="e">Event arguments containing the flight information.</param>
        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            FlightLanded?.Invoke(this, e);
        }
    }
}
