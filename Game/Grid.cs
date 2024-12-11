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

        private string[,] grid;

        private Boolean gridInitialized = false;


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
        public void initGrid()
        {
            if (gridInitialized) return;

            this.grid = new string[width, height];

            //for (int i = 0; i < width; i++)
            //{
            //    for (int ii = 0; y < height)
            //}

            gridInitialized = true;
        }

        List<Highlander> getCurrentPlayers()
        {
            return this.currentPlayers;
        }

        public int currentNumOfPlayers()
        {
            return this.currentPlayers.Count;
        }

        public void printGrid()
        {
            Console.WriteLine("Grid print");

            Console.WriteLine("DEBUG: current length of outer is {0}", this.grid.GetLength(0));
            Console.WriteLine("DEBUG: current length of inner is {0}", this.grid.GetLength(1));

            for (int i = 0; i < this.grid.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.grid.GetLength(1); ii++)
                {

                    //Console.WriteLine("({0},{1}) = {2} ", i, ii, this.grid[i, ii]);
                    Console.Write("'{0}', ", this.grid[i, ii]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        public void placePlayer(Highlander h)
        {
            Console.WriteLine("Place Player Method entered...");

            Console.WriteLine("BEFORE PLAYER ADDED: Length of array: " + this.currentPlayers.Count());

            Console.WriteLine("Player info: x: {0}, y: {1}...", h.X, h.Y);

            // checks if specified position is filled
            if (this.grid[h.X, h.Y] != null)
            {
                Console.WriteLine("DEBUG: Position is filled, cannot place player...");
                //Console.WriteLine("Position is empty...");
                return;
            }

            this.grid[h.X, h.Y] = (h.IsGood) ? "Good" : "Evil";

            Console.WriteLine("Testing contents of location [{0},{1}] = {2}", h.X, h.Y, grid[h.X, h.Y]);
            this.currentPlayers.Add(h);
            
        }

        /// <summary>
        /// Updates Grid and player positions specified in the currentPlayers list
        /// </summary>
        public void gridUpdate()
        {

            Console.WriteLine("Grid update method...");

            // validates location on grid for each highlander currently in list
            foreach (Highlander h in this.currentPlayers.ToList())
            {
                Console.WriteLine("x: {0}, y: {1}...", h.X, h.Y);
                if (this.grid[h.X, h.Y] == (h.IsGood ? "Good" : "Evil"))
                {
                    Console.WriteLine("DEBUG: {0} Highlander is in correct position on grid ({1}, {2})...", (h.IsGood ? "Good" : "Evil"), h.X, h.Y);
                } 
                else
                {
                    Console.WriteLine("DEBUG: {0} Highlander is not in correct position on grid ({1}, {2})...", (h.IsGood ? "Good" : "Evil"), h.X, h.Y);
                    this.grid[h.prevX, h.prevY] = ""; // sets previous position as empty
                    this.grid[h.X, h.Y] = (h.IsGood ? "Good" : "Evil");
                }
            }
        }
    }

}
