using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighlanderMovements;

namespace Game
{
    public class Grid
    {

        public int width { get; private set; }
        public int height { get; private set; }

        // contains players on current grid
        private List<HighlanderMovements.Highlander> currentPlayers = new List<Highlander>();

        private string[][] grid;


        public Grid(int x, int y)
        {
            this.width = x;
            this.height = y;
        }

        public Grid(int x, int y, List<Highlander> playerList)
        {
            this.width = x;
            this.height = y;
            this.currentPlayers = playerList;
        }

        // needs testing
        private void initGrid(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                this.grid[i] = new string[y];
            }
        }

        List<Highlander> getCurrentPlayers()
        {
            return this.currentPlayers;
        }

        public void printGrid()
        {
            Console.WriteLine("Grid print");
            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int ii = 0; i < this.grid.GetLength(1); ii++)
                {
                    Console.Write("{0}, ", this.grid[i][ii]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }
    }
}
