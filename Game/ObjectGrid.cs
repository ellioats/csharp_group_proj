using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class ObjectGrid
    {

        private Tile[,] grid;

        public ObjectGrid(int width, int height) {
            this.grid = new Tile[width, height];
            initGrid();
        }

        private void initGrid()
        {
            foreach (Tile i in this.grid)
            {
                i.isOccupied = false;
            }
        }

        public void placePlayer(HighlanderMovements.Highlander player)
        {

            this.grid[player.X, player.Y] = new PlayerTile(player);
            this.grid[player.X, player.Y].isOccupied = true;

        }

        public HighlanderMovements.Highlander getPlayer(int x, int y)
        {
            // if highlander is occupying specified cell
            if (grid[x,y].isOccupied)
            {
                PlayerTile temp = (PlayerTile) grid[x, y];
                return temp.getPlayer();
            }

            // player does not exist at location
            return null;
        }


    }
}
