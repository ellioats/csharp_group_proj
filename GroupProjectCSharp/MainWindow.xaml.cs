using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Simulation;

namespace ArrayTraveler
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private Simulation.Simulation simulation;
        private int width;
        private int height;
        private Random random = new Random();

        public MainWindow(int number1, int number2, int goodHighlanders, int badHighlanders)
        {
            InitializeComponent();
            width = number1;
            height = number2;
            InitializeSimulation(goodHighlanders, badHighlanders);
            PopulateGrid();
        }

        private void InitializeSimulation(int goodHighlanders, int badHighlanders)
        {
            simulation = new Simulation.Simulation(0, 0, width, height, Log);

            for (int i = 0; i < goodHighlanders; i++)
            {
                simulation.AddHighlander(random.Next(width), random.Next(height), true);
            }

            for (int i = 0; i < badHighlanders; i++)
            {
                simulation.AddHighlander(random.Next(width), random.Next(height), false);
            }

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!simulation.AreAllBadHighlandersDefeated())
            {
                simulation.MovePlayerRandomly();
                for (int i = 0; i < simulation.HighlandersCount; i++)
                {
                    simulation.MoveHighlanderRandomly(i);
                }
                UpdateGrid();
            }
            else
            {
                timer.Stop();
                Log("All bad Highlanders defeated!");
            }
        }

        private void PopulateGrid()
        {
            ArrayGrid.RowDefinitions.Clear();
            ArrayGrid.ColumnDefinitions.Clear();
            ArrayGrid.Children.Clear();

            for (int i = 0; i < height; i++)
            {
                ArrayGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < width; j++)
            {
                ArrayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            ArrayGrid.Children.Clear();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var cellContent = GetCellContent(i, j);
                    var cellBorder = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Child = new TextBlock
                        {
                            Text = cellContent,
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

        private string GetCellContent(int x, int y)
        {
            var playerPosition = simulation.GetPlayerPosition();
            if (playerPosition == (x, y))
            {
                return "P";
            }

            for (int i = 0; i < simulation.HighlandersCount; i++)
            {
                var highlander = simulation.GetHighlander(i);
                if (highlander.X == x && highlander.Y == y)
                {
                    return highlander.IsGood ? "G" : "B";
                }
            }

            return string.Empty;
        }

        private void StartSimulationButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Log(string message)
        {
            LogTextBox.AppendText(message + Environment.NewLine);
            LogTextBox.ScrollToEnd();
        }
    }
}