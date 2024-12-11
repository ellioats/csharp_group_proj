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

        List<Highlander> getCurrentPlayers()
        {
            return this.currentPlayers;
        }


    }
}
