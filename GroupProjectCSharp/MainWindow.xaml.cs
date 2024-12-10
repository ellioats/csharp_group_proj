using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HighlanderMovements;

namespace GroupProjectCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // temporary testing function for HighlanderMovements component

            string[][] area = { 
                ["", "8", "7", "6", "", ""], 
                ["", "1", "P", "5", "", ""],
                ["", "2", "3", "4", "", ""], 
                ["", "", "", "", "", ""], 
                ["", "", "", "", "", ""], 
                ["", "", "", "", "", ""] 
            };

            int CurrentPlayerX = 2;
            int CurrentPlayerY = 2;

            Boolean ?isEvil = null;

            string outputText = "";



            HighlanderMovements.Class1.isHighlanderNear(ref area, CurrentPlayerX, CurrentPlayerY, ref isEvil, ref outputText);

            textBlock.Text = outputText;
        }
    }
}