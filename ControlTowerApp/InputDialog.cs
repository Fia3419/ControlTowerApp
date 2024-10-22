using System.Windows;
using System.Windows.Controls;

namespace ControlTowerApp
{
    public static class InputDialog
    {
        public static string Show(string title, string promptText)
        {
            // Create the input dialog window
            Window inputDialog = new Window()
            {
                Width = 300,
                Height = 150,
                Title = title,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Create stack panel to arrange elements
            StackPanel stackPanel = new StackPanel();

            // Prompt text for the user
            TextBlock prompt = new TextBlock() { Text = promptText };

            // Input box for user text
            TextBox input = new TextBox();

            // OK button for confirmation
            Button confirmation = new Button() { Content = "OK", Width = 100, IsDefault = true, IsCancel = true };

            // Add elements to stack panel
            stackPanel.Children.Add(prompt);
            stackPanel.Children.Add(input);
            stackPanel.Children.Add(confirmation);

            // Set the window content to the stack panel
            inputDialog.Content = stackPanel;

            string result = null;

            // Event handler for when OK button is clicked
            confirmation.Click += (sender, e) =>
            {
                result = input.Text;  // Get user input
                inputDialog.DialogResult = true;  // Close dialog
                inputDialog.Close();
            };

            inputDialog.ShowDialog();
            return result;  // Return the user input
        }
    }
}
