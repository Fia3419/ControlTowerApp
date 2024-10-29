using System;
using System.Windows;
using ControlTowerBLL;
using ControlTowerDTO;
using ControlTowerServices;

namespace ControlTowerApp
{
    /// <summary>
    /// Interaction logic for the MainWindow class, handling the user interface.
    /// It manages flight operations by interacting with the FlightService class.
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightService flightService;

        /// <summary>
        /// Initializes a new instance of the MainWindow class and subscribes to flight events.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            flightService = new FlightService();
            flightService.SubscribeToTakeOff(OnFlightTakeOff);
            flightService.SubscribeToLanding(OnFlightLanded);
        }

        /// <summary>
        /// Handles the Add Plane button click event. Adds a new flight to the system.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">Event arguments for the button click.</param>
        private void btnAddPlane_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAirliner.Text) ||
                string.IsNullOrWhiteSpace(txtFlightId.Text) ||
                string.IsNullOrWhiteSpace(txtDestination.Text) ||
                !UtilitiesLib.InputParser.TryParseDecimal(txtDuration.Text, out decimal duration))
            {
                MessageBox.Show("Please provide valid flight details.");
                return;
            }

            // Create and add a new flight using FlightDTO
            FlightDTO flightDTO = new FlightDTO
            {
                Airliner = txtAirliner.Text,
                Id = txtFlightId.Text,
                Destination = txtDestination.Text,
                Duration = (double)duration
            };
            flightService.AddFlight(flightDTO);
            lvFlights.Items.Add(flightDTO);
            btnTakeOff.IsEnabled = true;
        }

        /// <summary>
        /// Handles the Take Off button click event to initiate flight takeoff.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">Event arguments for the button click.</param>
        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            if (lvFlights.SelectedItem is FlightDTO selectedFlight && !selectedFlight.InFlight)
            {
                flightService.TakeOffFlight(selectedFlight);
                btnTakeOff.IsEnabled = false;
                btnNewHeight.IsEnabled = true;
            }
        }

        /// <summary>
        /// Handles the New Height button click event to change the flight's altitude.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">Event arguments for the button click.</param>
        private void btnNewHeight_Click(object sender, RoutedEventArgs e)
        {
            if (lvFlights.SelectedItem is FlightDTO selectedFlight)
            {
                string inputHeight = InputDialog.Show("Enter New Flight Height", "Please enter the new flight height:");

                if (int.TryParse(inputHeight, out int newHeight))
                {
                    flightService.ChangeFlightHeight(selectedFlight, newHeight);
                    lvStatusUpdates.Items.Add($"Flight {selectedFlight.Airliner} (Flight ID: {selectedFlight.Id}) changed altitude to {newHeight}.");
                }
                else
                {
                    MessageBox.Show("Invalid height. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Handles the FlightTakeOff event, updating the status of a flight.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event data related to the flight takeoff.</param>
        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string message = $"Flight {e.Flight.FlightData.Airliner} (Flight ID: {e.Flight.FlightData.Id}) has departed for {e.Flight.FlightData.Destination} at {e.Flight.FlightData.DepartureTime:HH:mm:ss}.";
                lvStatusUpdates.Items.Add(message);
            });
        }

        /// <summary>
        /// Handles the FlightLanded event, updating the status of a flight.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Event data related to the flight landing.</param>
        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string message = $"Flight {e.Flight.FlightData.Airliner} (Flight ID: {e.Flight.FlightData.Id}) has landed at {DateTime.Now:HH:mm:ss}.";
                lvStatusUpdates.Items.Add(message);
                btnTakeOff.IsEnabled = true;
                btnNewHeight.IsEnabled = false;
                e.Flight.FlightData.Destination = "Home";
            });
        }
    }
}
