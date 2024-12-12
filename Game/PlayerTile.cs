using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class PlayerTile : Tile
    {

        public bool isOccupied = false; // by default, all tiles are empty

        private HighlanderMovements.Highlander occupant;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">highlander is expected to be fully initialized</param>
        public PlayerTile(HighlanderMovements.Highlander player) : base(player.X, player.Y) 
        {
            this.isOccupied = true;
            this.occupant = player;
        }

        
    }
}
