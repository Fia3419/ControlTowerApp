using System.Collections.Generic;
using ControlTowerDTO;

namespace ControlTowerDAL
{
    public class FlightRepository
    {
        private List<FlightDTO> flights = new List<FlightDTO>();

        public void AddFlight(FlightDTO flight)
        {
            flights.Add(flight);
        }

        public List<FlightDTO> GetFlights()
        {
            return flights;
        }
    }
}