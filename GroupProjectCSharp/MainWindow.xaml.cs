using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HighlanderMovements;

namespace GroupProjectCSharp
{
    public partial class MainWindow : Window
    {
        private Grid gameGrid;

        public MainWindow(int width, int height, int goodHighlanders, int badHighlanders)
        {
            InitializeComponent();
            InitializeGame(width, height, goodHighlanders, badHighlanders);
        }

        private void InitializeGame(int width, int height, int goodHighlanders, int badHighlanders)
        {
            gameGrid = new Grid(width, height);
            gameGrid.initGrid();

            List<Highlander> players = new List<Highlander>();
            Random rand = new Random();

            for (int i = 0; i < goodHighlanders; i++)
            {
                int[] coords = gameGrid.getRandomXY();
                players.Add(new Highlander(coords[0], coords[1], true));
            }

            for (int i = 0; i < badHighlanders; i++)
            {
                int[] coords = gameGrid.getRandomXY();
                players.Add(new Highlander(coords[0], coords[1], false));
            }

            gameGrid.setPlayerList(players);

            foreach (var player in players)
            {
                gameGrid.placePlayer(player);
            }

            UpdateGridView();
        }

        private void UpdateGridView()
        {
            GameGrid.Columns.Clear();
            for (int i = 0; i < gameGrid.width; i++)
            {
                GameGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = $"Column {i + 1}",
                    Binding = new System.Windows.Data.Binding($"[{i}]")
                });
            }

            var gridData = new List<string[]>();
            for (int j = 0; j < gameGrid.height; j++)
            {
                var row = new string[gameGrid.width];
                for (int i = 0; i < gameGrid.width; i++)
                {
                    row[i] = gameGrid.getGrid()[i, j];
                }
                gridData.Add(row);
            }

            GameGrid.ItemsSource = gridData;
        }

        private void MovePlayers_Click(object sender, RoutedEventArgs e)
        {
            foreach (var player in gameGrid.getCurrentPlayers())
            {
                player.MoveRandomly(gameGrid.width, gameGrid.height);
            }
            gameGrid.gridUpdate();
            UpdateGridView();
        }
    }
}