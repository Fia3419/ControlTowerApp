using System;
using System.Collections.Generic;
using ControlTowerDTO;
using ControlTowerBLL.Managers;
using ControlTowerDAL;

namespace ControlTowerBLL
{
    public class ControlTower : ListManager<Flight>
    {
        private FlightRepository flightRepository = new FlightRepository();

        public event EventHandler<TakeOffEventArgs> FlightTakeOff;
        public event EventHandler<LandedEventArgs> FlightLanded;
        public event EventHandler<FlightHeightChangedEventArgs> FlightHeightChanged;

        public void AddFlight(Flight flight)
        {
            flight.TakeOff += OnFlightTakeOff;
            flight.Landed += OnFlightLanded;
            flight.FlightHeightChanged += OnFlightHeightChanged;
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
            Flight flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration);
            flight.TakeOffFlight();
            FlightTakeOff?.Invoke(this, new TakeOffEventArgs(flight));
        }

        public void LandFlight(FlightDTO flightDTO)
        {
            flightDTO.InFlight = false;
            FlightLanded?.Invoke(this, new LandedEventArgs(new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration)));
        }

        public void ChangeFlightHeight(FlightDTO flightDTO, int newHeight)
        {
            var flight = new Flight(flightDTO.Airliner, flightDTO.Id, flightDTO.Destination, flightDTO.Duration);
            flight.ChangeFlightHeight(newHeight);
            FlightHeightChanged?.Invoke(this, new FlightHeightChangedEventArgs(flight, newHeight));
        }

        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            FlightTakeOff?.Invoke(this, e);
        }

        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            FlightLanded?.Invoke(this, e);
        }

        private void OnFlightHeightChanged(object sender, FlightHeightChangedEventArgs e)
        {
            FlightHeightChanged?.Invoke(this, e);
        }
    }
}
