using System;
using System.Windows.Threading;
using ControlTowerDTO;
namespace ControlTowerBLL
{
    public class Flight
    {
        public event EventHandler<TakeOffEventArgs> FlightTakeOff;
        public event EventHandler<LandedEventArgs> FlightLanded;

        private DispatcherTimer dispatchTimer;
        private double flightProgress;

        public FlightDTO FlightData { get; private set; }
        public int FlightHeight { get; private set; }

        public string Airliner
        {
            get => FlightData.Airliner;
            set => FlightData.Airliner = value;
        }

        public string Id
        {
            get => FlightData.Id;
            set => FlightData.Id = value;
        }

        public string Destination
        {
            get => FlightData.Destination;
            set => FlightData.Destination = value;
        }

        public double Duration
        {
            get => FlightData.Duration;
            set => FlightData.Duration = value;
        }

        public bool InFlight
        {
            get => FlightData.InFlight;
            set => FlightData.InFlight = value;
        }

        public DateTime DepartureTime
        {
            get => FlightData.DepartureTime;
            set => FlightData.DepartureTime = value;
        }

        public Flight(string airliner, string id, string destination, double duration)
        {
            FlightData = new FlightDTO
            {
                Airliner = airliner,
                Id = id,
                Destination = destination,
                Duration = duration,
                InFlight = false
            };
            flightProgress = 0;
        }

        public void SetupTimer()
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += DispatcherTimer_Tick;
            dispatchTimer.Interval = TimeSpan.FromSeconds(1);
            dispatchTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            flightProgress += 1;
            if (flightProgress >= FlightData.Duration)
            {
                OnLanding();
            }
        }

        public void TakeOffFlight()
        {
            InFlight = true;
            DepartureTime = DateTime.Now;
            flightProgress = 0;
            OnTakeOff();
            SetupTimer();
        }

        public void LandFlight()
        {
            OnLanding();
        }

        public void ChangeFlightHeight(int newHeight)
        {
            FlightHeight = newHeight;
        }

        protected virtual void OnTakeOff()
        {
            FlightTakeOff?.Invoke(this, new TakeOffEventArgs(this));
        }

        protected virtual void OnLanding()
        {
            InFlight = false;
            FlightLanded?.Invoke(this, new LandedEventArgs(this));
            dispatchTimer.Stop();
        }
    }
}