using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Tile
    {

        private string status = "";

        public bool isOccupied = false; // by default, all tiles are empty

        // x and y should only be set in constructor
        private int x { get; }
        private int y { get; }

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }

        public string getStatus()
        {
            return this.status;
        }

        public int GetX()
        {
            return this.x;
        }

        public int GetY() {
            return this.y;
        }


    }
}
