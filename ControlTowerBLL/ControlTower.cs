using System;
using System.Collections.Generic;
using ControlTowerDAL;
using ControlTowerDTO;
using ControlTowerBLL.Managers;

namespace ControlTowerBLL
{
    public class ControlTower : ListManager<Flight>
    {
        private FlightRepository flightRepository = new FlightRepository();

        public event EventHandler<FlightEventArgs> FlightTakeOff;
        public event EventHandler<FlightEventArgs> FlightLanded;

        public void AddFlight(Flight flight)
        {
            flight.TakeOff += OnFlightTakeOff;
            flight.Landed += OnFlightLanded;
            Add(flight);
        }

        public void TakeOffFlight(Flight flight)
        {
            flight.TakeOffFlight();
        }

        public void AddFlight(FlightDTO flightDTO)
        {
            Flight flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration);
            AddFlight(flight);
            flightRepository.AddFlight(flightDTO);
        }

        public List<FlightDTO> GetFlights()
        {
            return flightRepository.GetFlights();
        }

        public void TakeOffFlight(FlightDTO flightDTO)
        {
            Flight flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration)
            {
                InFlight = flightDTO.InFlight,
                DepartureTime = flightDTO.DepartureTime
            };
            TakeOffFlight(flight);
            flightDTO.InFlight = true;
            flightDTO.DepartureTime = DateTime.Now;
            FlightTakeOff?.Invoke(this, new FlightEventArgs(flight));
        }

        public void LandFlight(FlightDTO flightDTO)
        {
            flightDTO.InFlight = false;
            FlightLanded?.Invoke(this, new FlightEventArgs(flightDTO));
        }

        private void OnFlightTakeOff(object sender, FlightEventArgs e)
        {
            FlightTakeOff?.Invoke(this, e);
        }

        private void OnFlightLanded(object sender, FlightEventArgs e)
        {
            FlightLanded?.Invoke(this, e);
        }
    }
}