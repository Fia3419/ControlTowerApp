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


        public event EventHandler<TakeOffEventArgs> FlightTakeOff;
        public event EventHandler<LandedEventArgs> FlightLanded;
        public event EventHandler<FlightHeightChangedEventArgs> FlightHeightChanged;

        public void AddFlight(Flight flight)
        {
            flight.FlightTakeOff += OnFlightTakeOff;
            flight.FlightLanded += OnFlightLanded;
            flight.FlightHeightChanged += OnFlightHeightChanged;
            Add(flight);
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
            Flight flight = items.Find(f => f.Id == flightDTO.Id);
            if (flight != null)
            {
                flight.TakeOffFlight();
                flightDTO.InFlight = true;
                flightDTO.DepartureTime = DateTime.Now;
            }
        }

        public void LandFlight(FlightDTO flightDTO)
        {
            Flight flight = items.Find(f => f.Id == flightDTO.Id);
            if (flight != null)
            {
                flight.LandFlight();
                flightDTO.InFlight = false;
                flightDTO.Destination = "Home";
                FlightLanded?.Invoke(this, new LandedEventArgs(flight));
            }
        }

        public void ChangeFlightHeight(FlightDTO flightDTO, int newHeight)
        {
            Flight flight = items.Find(f => f.Id == flightDTO.Id);
            if (flight != null)
            {
                flight.ChangeFlightHeight(newHeight);
            }
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