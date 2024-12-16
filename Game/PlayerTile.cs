using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class PlayerTile : Tile
    {


        private HighlanderMovements.Highlander occupant;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">highlander is expected to be fully initialized</param>
        public PlayerTile(HighlanderMovements.Highlander player) : base(player.X, player.Y) 
        {
            base.isOccupied = true;
            this.occupant = player;
        }
        
        public HighlanderMovements.Highlander getPlayer()
        {
            return this.occupant;
        }

        public void addPlayerToTile(HighlanderMovements.Highlander player)
        {
            base.isOccupied = true;
            this.occupant = player;
        }

        public void setOccupant(HighlanderMovements.Highlander a)
        {
            this.occupant = a;
        }

        
    }
}
