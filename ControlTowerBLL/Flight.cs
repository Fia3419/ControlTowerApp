using System;
using System.Windows.Threading;
using ControlTowerDTO;

namespace ControlTowerBLL
{
    /// <summary>
    /// Represents a flight with data related to its state, such as altitude, duration, and whether it's in flight.
    /// Handles the takeoff and landing operations with events.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Event triggered when the flight takes off.
        /// </summary>
        public event EventHandler<TakeOffEventArgs> FlightTakeOff;

        /// <summary>
        /// Event triggered when the flight lands.
        /// </summary>
        public event EventHandler<LandedEventArgs> FlightLanded;

        private DispatcherTimer dispatchTimer;
        private double flightProgress;

        /// <summary>
        /// Contains the flight data such as airliner, ID, destination, duration, and more.
        /// </summary>
        public FlightDTO FlightData { get; private set; }

        /// <summary>
        /// The current altitude of the flight.
        /// </summary>
        public int FlightHeight { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Flight class with provided details.
        /// </summary>
        /// <param name="airliner">The airline operating the flight.</param>
        /// <param name="id">The unique identifier for the flight.</param>
        /// <param name="destination">The destination of the flight.</param>
        /// <param name="duration">The duration of the flight in hours.</param>
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

        /// <summary>
        /// Starts the takeoff process and triggers the FlightTakeOff event.
        /// </summary>
        public void TakeOffFlight()
        {
            FlightData.InFlight = true;
            FlightData.DepartureTime = DateTime.Now;
            flightProgress = 0;
            SetupTimer();
            FlightTakeOff?.Invoke(this, new TakeOffEventArgs(this));
        }

        /// <summary>
        /// Changes the flight's altitude to a new value.
        /// </summary>
        /// <param name="newHeight">The new altitude for the flight.</param>
        public void ChangeFlightHeight(int newHeight)
        {
            FlightHeight = newHeight;
        }

        /// <summary>
        /// Initiates the landing process and triggers the FlightLanded event.
        /// </summary>
        public void LandFlight()
        {
            FlightData.InFlight = false;
            StopTimer();
            FlightLanded?.Invoke(this, new LandedEventArgs(this));
        }

        /// <summary>
        /// Sets up the timer for the flight, simulating flight time.
        /// </summary>
        private void SetupTimer()
        {
            dispatchTimer = new DispatcherTimer();
            dispatchTimer.Tick += OnTimerTick;
            dispatchTimer.Interval = TimeSpan.FromSeconds(1);
            dispatchTimer.Start();
        }

        /// <summary>
        /// Event handler for each timer tick, simulating flight progress.
        /// </summary>
        private void OnTimerTick(object sender, EventArgs e)
        {
            flightProgress++;
            if (flightProgress >= FlightData.Duration)
            {
                LandFlight();
            }
        }

        /// <summary>
        /// Stops the flight timer.
        /// </summary>
        private void StopTimer()
        {
            dispatchTimer.Stop();
        }
    }
}
