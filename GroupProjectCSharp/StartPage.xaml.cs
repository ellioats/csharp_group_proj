using ArrayTraveler;
using System.Windows;
using System.Windows.Controls;

namespace GroupProjectCSharp
{
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null && (textBox.Text == "Enter first integer" || textBox.Text == "Enter second integer"))
            {
                textBox.Text = string.Empty;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TextBox1.Text, out int number1) && int.TryParse(TextBox2.Text, out int number2))
            {
                MainWindow mainWindow = new MainWindow(number1, number2);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid integers in both text boxes.");
            }
        }
    }
}