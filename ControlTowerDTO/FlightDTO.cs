namespace ControlTowerDTO
{
    /// <summary>
    /// Data Transfer Object for a flight, holding the details of the flight, such as airliner, destination, and duration.
    /// </summary>
    public class FlightDTO
    {
        /// <summary>
        /// Gets or sets the airliner operating the flight.
        /// </summary>
        public string Airliner { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the flight.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the destination of the flight.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the duration of the flight in hours.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the flight is currently in the air.
        /// </summary>
        public bool InFlight { get; set; }

        /// <summary>
        /// Gets or sets the departure time of the flight.
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the current altitude of the flight.
        /// </summary>
        public int FlightHeight { get; set; }
    }
}
