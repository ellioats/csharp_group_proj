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
            if (textBox != null && (textBox.Text == "Enter first integer" || textBox.Text == "Enter second integer" || textBox.Text == "Enter number of good Highlanders" || textBox.Text == "Enter number of bad Highlanders"))
            {
                textBox.Text = string.Empty;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TextBox1.Text, out int number1) && int.TryParse(TextBox2.Text, out int number2) &&
                int.TryParse(GoodHighlandersTextBox.Text, out int goodHighlanders) && int.TryParse(BadHighlandersTextBox.Text, out int badHighlanders))
            {
                MainWindow mainWindow = new MainWindow(number1, number2, goodHighlanders, badHighlanders);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid integers in all text boxes.");
            }
        }
    }
}