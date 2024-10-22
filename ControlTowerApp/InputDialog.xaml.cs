using System.Windows;

namespace ControlTowerApp
{
    public partial class InputDialog : Window
    {
        public string Input { get; private set; }

        public InputDialog(string promptText, string title)
        {
            InitializeComponent();
            this.Title = title;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Input = txtInput.Text;
            this.DialogResult = true;
            this.Close();
        }

        public static string Show(string title, string promptText)
        {
            InputDialog inputDialog = new InputDialog(promptText, title);
            if (inputDialog.ShowDialog() == true)
            {
                return inputDialog.Input;
            }
            return null;
        }
    }
}
