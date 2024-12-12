using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Tile
    {

        public bool isOccupied = false; // by default, all tiles are empty

        // x and y should only be set in constructor
        private int x { get; }
        private int y { get; }

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


    }
}
