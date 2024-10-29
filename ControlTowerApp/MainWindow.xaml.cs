using System;
using System.Windows;
using ControlTowerBLL;
using ControlTowerDTO;

namespace ControlTowerApp
{
    public partial class MainWindow : Window
    {
        private ControlTower controlTower = new ControlTower();

        public MainWindow()
        {
            InitializeComponent();
            controlTower.FlightTakeOff += OnFlightTakeOff;
            controlTower.FlightLanded += OnFlightLanded;
            controlTower.FlightHeightChanged += OnFlightHeightChanged;
        }

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

            var flight = new Flight(txtAirliner.Text, txtFlightId.Text, txtDestination.Text, (double)duration);
            controlTower.AddFlight(flight);
            lvFlights.Items.Add(flight.FlightData);
            btnTakeOff.IsEnabled = true;
        }

        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            if (lvFlights.SelectedItem is FlightDTO selectedFlight && !selectedFlight.InFlight)
            {
                controlTower.TakeOffFlight(selectedFlight);
                btnTakeOff.IsEnabled = false;
                btnNewHeight.IsEnabled = true;
            }
        }

        private void btnNewHeight_Click(object sender, RoutedEventArgs e)
        {
            if (lvFlights.SelectedItem is FlightDTO selectedFlight)
            {
                string inputHeight = InputDialog.Show("Enter New Flight Height", "Please enter the new flight height:");

                if (int.TryParse(inputHeight, out int newHeight))
                {
                    controlTower.ChangeFlightHeight(selectedFlight, newHeight);
                    lvStatusUpdates.Items.Add($"Flight {selectedFlight.Airliner} (Flight ID: {selectedFlight.Id}) changed altitude to {newHeight}.");
                }
                else
                {
                    MessageBox.Show("Invalid height. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnFlightTakeOff(object sender, TakeOffEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string message = $"Flight {e.Flight.Airliner} (Flight ID: {e.Flight.Id}) has departed for {e.Flight.Destination} at {e.Flight.DepartureTime:HH:mm:ss}.";
                lvStatusUpdates.Items.Add(message);
                Console.WriteLine("Takeoff message added to ListView.");
            });
        }

        private void OnFlightLanded(object sender, LandedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine("Inside Dispatcher for landing event.");

                if (e.Flight != null)
                {
                    string message = $"Flight {e.Flight.Airliner} (Flight ID: {e.Flight.Id}) has landed at {DateTime.Now:HH:mm:ss}.";
                    lvStatusUpdates.Items.Add(message);
                    lvStatusUpdates.UpdateLayout();
                    Console.WriteLine("Landing message added to ListView.");

                    // Allow the flight to take off again after landing
                    btnTakeOff.IsEnabled = true;
                    btnNewHeight.IsEnabled = false;

                    // Update the destination to "Home"
                    e.Flight.Destination = "Home";
                }
                else
                {
                    Console.WriteLine("Flight object is null in OnFlightLanded.");
                }
            });
        }

        private void OnFlightHeightChanged(object sender, FlightHeightChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                lvStatusUpdates.Items.Add($"Flight {e.Flight.Airliner} (Flight ID: {e.Flight.Id}) changed altitude to {e.NewHeight}.");
            });
        }
    }
}