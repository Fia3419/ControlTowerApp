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

        public void TakeOffFlight()
        {
            FlightData.InFlight = true;
            FlightData.DepartureTime = DateTime.Now;
            flightProgress = 0;
            SetupTimer();
            FlightTakeOff?.Invoke(this, new TakeOffEventArgs(this));
        }

        public void ChangeFlightHeight(int newHeight)
        {
            FlightHeight = newHeight;
        }

        public void LandFlight()
        {
            FlightData.InFlight = false;
            StopTimer();
            FlightLanded?.Invoke(this, new LandedEventArgs(this));
        }

        private void SetupTimer()
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += OnTimerTick;
            dispatchTimer.Interval = TimeSpan.FromSeconds(1);
            dispatchTimer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            flightProgress++;
            if (flightProgress >= FlightData.Duration)
            {
                LandFlight();
            }
        }

        private void StopTimer()
        {
            dispatchTimer.Stop();
        }
    }
}
