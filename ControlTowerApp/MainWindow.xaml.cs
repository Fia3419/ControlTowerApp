using System;
using System.Windows;
using ControlTowerBLL;

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
        }

        private void btnAddPlane_Click(object sender, RoutedEventArgs e)
        {
            var flight = new Flight(txtAirliner.Text, txtFlightId.Text, txtDestination.Text, double.Parse(txtDuration.Text));
            controlTower.AddFlight(flight);
            lvFlights.Items.Add(flight);
            btnTakeOff.IsEnabled = true;
            btnNewHeight.IsEnabled = true;
        }

        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            if (lvFlights.SelectedItem is Flight selectedFlight)
            {
                controlTower.TakeOffFlight(selectedFlight);
                btnTakeOff.IsEnabled = false;
                btnNewHeight.IsEnabled = false;
            }
        }

        private void btnNewHeight_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic for changing the flight height here
        }

        private void OnFlightTakeOff(object sender, FlightEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                txtLog.Text += $"Flight {e.Flight.Airliner} (Flight ID: {e.Flight.Id}) has departed for {e.Flight.Destination} at {e.Flight.DepartureTime:HH:mm:ss}.\n";
            });
        }

        private void OnFlightLanded(object sender, FlightEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                txtLog.Text += $"Flight {e.Flight.Airliner} (Flight ID: {e.Flight.Id}) has landed at {e.Flight.Destination} at {DateTime.Now:HH:mm:ss}.\n";
            });
        }
    }
}