using System;
using System.Windows.Threading;
using ControlTowerDTO;

namespace ControlTowerBLL
{
    public class Flight
    {
        public FlightDTO FlightData { get; private set; }

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

        public event EventHandler<FlightEventArgs> TakeOff;
        public event EventHandler<FlightEventArgs> Landed;

        private DispatcherTimer dispatchTimer;

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
        }

        public void SetupTimer()
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += DispatcherTimer_Tick;
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);
            dispatchTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - DepartureTime).TotalSeconds >= Duration)
            {
                OnLanding();
            }
        }

        public void TakeOffFlight()
        {
            InFlight = true;
            DepartureTime = DateTime.Now;
            OnTakeOff();
            SetupTimer();
        }

        protected virtual void OnTakeOff()
        {
            TakeOff?.Invoke(this, new FlightEventArgs(this));
        }

        protected virtual void OnLanding()
        {
            InFlight = false;
            Landed?.Invoke(this, new FlightEventArgs(this));
            dispatchTimer.Stop();
        }
    }
}