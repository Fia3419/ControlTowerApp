namespace ControlTowerDTO
{
    public class FlightDTO
    {
        public string Airliner { get; set; }
        public string Id { get; set; }
        public string Destination { get; set; }
        public double Duration { get; set; }
        public bool InFlight { get; set; }
        public DateTime DepartureTime { get; set; }
        public int FlightHeight { get; set; }
    }
}