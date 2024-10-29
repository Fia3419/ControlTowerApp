using System;
using System.Collections.Generic;
using ControlTowerDAL;
using ControlTowerDTO;
using ControlTowerBLL.Managers;

namespace ControlTowerBLL
{
    public class ControlTower : ListManager<Flight>
    {
        private readonly FlightRepository flightRepository = new FlightRepository();
        public event EventHandler<TakeOffEventArgs> FlightTakeOff;
        public event EventHandler<LandedEventArgs> FlightLanded;
        public delegate int ChangeAltitudeDelegate(int currentAltitude, int changeValue);
        public ChangeAltitudeDelegate ChangeAltitudeHandler { get; private set; }

        public ControlTower()
        {
            ChangeAltitudeHandler = ChangeAltitude;
        }

        private int ChangeAltitude(int currentAltitude, int changeValue)
        {
            return currentAltitude + changeValue;
        }

        public void AddFlight(Flight flight)
        {
            SubscribeToFlightEvents(flight);
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

        public void InitiateTakeOff(FlightDTO flightDTO)
        {
            Flight flight = FindFlightById(flightDTO.Id);
            if (flight != null && !flight.InFlight)
            {
                flight.TakeOffFlight();
                flightDTO.InFlight = true;
                flightDTO.DepartureTime = DateTime.Now;
            }
        }

        public void LandFlight(FlightDTO flightDTO)
        {
            Flight flight = FindFlightById(flightDTO.Id);
            if (flight != null)
            {
                flight.LandFlight();
                flightDTO.InFlight = false;
                flightDTO.Destination = "Home";
                FlightLanded?.Invoke(this, new LandedEventArgs(flight));
            }
        }

        public void ChangeFlightHeight(FlightDTO flightDTO, int changeValue)
        {
            Flight flight = FindFlightById(flightDTO.Id);
            if (flight != null && flight.InFlight)
            {
                int newAltitude = ChangeAltitudeHandler(flight.FlightHeight, changeValue);
                flight.ChangeFlightHeight(newAltitude);
            }
        }

        private void SubscribeToFlightEvents(Flight flight)
        {
            flight.FlightTakeOff += OnFlightTakeOff;
            flight.FlightLanded += OnFlightLanded;
        }

        private Flight FindFlightById(string flightId)
        {
            return items.Find(f => f.Id == flightId);
        }

        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            FlightTakeOff?.Invoke(this, e);
        }

        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            FlightLanded?.Invoke(this, e);
        }
    }
}