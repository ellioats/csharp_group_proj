using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArrayTraveler
{
    public partial class MainWindow : Window
    {
        private string[,] array;

        public MainWindow(int number1, int number2)
        {
            InitializeComponent();
            array = new string[number1, number2];
            InitializeArray();
            PopulateGrid();
        }

        private void InitializeArray()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = "Bitch";
                }
            }
        }

        private void PopulateGrid()
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                ArrayGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < array.GetLength(1); j++)
            {
                ArrayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    var cellBorder = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Child = new TextBlock
                        {
                            Text = array[i, j].ToString(),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        }
                    };
                    Grid.SetRow(cellBorder, i);
                    Grid.SetColumn(cellBorder, j);
                    ArrayGrid.Children.Add(cellBorder);
                }
            }
        }
    }
}