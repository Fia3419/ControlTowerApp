
namespace ControlTowerBLL
{
    /// <summary>
    /// Provides data for the FlightTakeOff event, containing information about the flight that has taken off.
    /// </summary>
    public class TakeOffEventArgs : EventArgs
    {
        /// <summary>
        /// The flight that has taken off.
        /// </summary>
        public Flight Flight { get; }

        /// <summary>
        /// Initializes a new instance of the TakeOffEventArgs class with the flight data.
        /// </summary>
        /// <param name="flight">The flight that has taken off.</param>
        public TakeOffEventArgs(Flight flight)
        {
            Flight = flight;
        }
    }
}