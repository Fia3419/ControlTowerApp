using System.Collections.Generic;
using ControlTowerDTO;

namespace ControlTowerDAL
{
    /// <summary>
    /// This class is responsible for managing the storage and retrieval of flight data.
    /// It simulates a data repository that stores flight information.
    /// </summary>
    public class FlightRepository
    {
        /// <summary>
        /// A list that holds all the flight data as <see cref="FlightDTO"/> objects.
        /// </summary>
        private List<FlightDTO> flights = new List<FlightDTO>();

        /// <summary>
        /// Adds a new flight to the repository.
        /// </summary>
        /// <param name="flight">The <see cref="FlightDTO"/> object representing the flight to add.</param>
        public void AddFlight(FlightDTO flight)
        {
            flights.Add(flight);
        }

        /// <summary>
        /// Retrieves the list of all flights stored in the repository.
        /// </summary>
        /// <returns>A list of <see cref="FlightDTO"/> objects representing the flights in the repository.</returns>
        public List<FlightDTO> GetFlights()
        {
            return flights;
        }
    }
}
