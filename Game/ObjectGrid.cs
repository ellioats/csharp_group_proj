using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ObjectGrid
    {

        private Tile[,] grid;

        private int width { get; set; }
        private int height { get; set; }

        Random rand;

        public ObjectGrid(int width, int height) {
            this.grid = new Tile[width, height];
            this.width = width;
            this.height = height;
            rand = new Random();

            InitGrid();
        }

        private void InitGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = new Tile(x,y);
                    grid[x, y].isOccupied = false;
                    grid[x, y].setStatus("empty");
                }
            }
        }

        public void PlacePlayer(HighlanderMovements.Highlander player)
        {

            this.grid[player.X, player.Y] = new PlayerTile(player);
            this.grid[player.X, player.Y].isOccupied = true;
            this.grid[player.X, player.Y].setStatus("player");

            // debug
            Console.WriteLine(player.Name + " placed at position: [{0}, {1}]", player.X, player.Y);
            Console.WriteLine("Status at position is: " + grid[player.X, player.Y].getStatus());

        }

        public HighlanderMovements.Highlander GetPlayer(int x, int y)
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

        // returns true if collision is detected
        //public bool ValidatePositions2()
        //{
        //    for (int x = 0; x < width;  x++)
        //    {
        //        for (int y = 0; y < height; y++)
        //        {
        //            if (this.grid[x,y].isOccupied)
        //            {
        //                PlayerTile temp = (PlayerTile) this.grid[x, y];
        //                // checks to make sure object is in correct space 
        //                if (x != temp.getPlayer().X || y != temp.getPlayer().Y)
        //                {
        //                    Console.WriteLine("{0}'s position is not valid...", temp.getPlayer().Name);
        //                    Console.WriteLine("Player current position is [{0}, {1}]", x, y);
        //                    Console.WriteLine("Player position should be [{0}, {1}]", temp.getPlayer().X, temp.getPlayer().Y);

        //                    // debug
        //                    if ((grid[x ,y] as PlayerTile).getPlayer() != null)
        //                    {
        //                        Console.WriteLine("Player detected at location: [{0}, {1}]", x, y);
        //                    }


        //                    // position is not valid
        //                    if ((grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile) is null)
        //                    {
        //                        Console.WriteLine("Prospected Position of [{0},{1}] does not contain player...", temp.getPlayer().X, temp.getPlayer().Y);
        //                        Console.WriteLine("Resetting position to new location");
        //                        this.ResetPlayerPosition(temp.getPlayer(), x, y);   
        //                    } else
        //                    {

        //                        Console.WriteLine("Collision occured. Fight should begin here...");

                                

        //                        // unused
        //                        //int defendingPlayerPower = (grid[x, y] as PlayerTile).getPlayer().Power;
        //                        //int attackingPlayerPower = (grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile).getPlayer().Power;

        //                        // sets position of defender depending on fight outcome
        //                        Console.WriteLine("Setting grid [{0}, {1}] to winner...", temp.getPlayer().X, temp.getPlayer().Y);
        //                        grid[temp.getPlayer().X, temp.getPlayer().Y] = new PlayerTile(fight(temp.getPlayer(), (grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile).getPlayer()));
                                
                                

        //                        //Console.WriteLine("Fight not implemented: temporary operation -> reverting position from prospect [{0}, {1}] -> [{2}, {3}]", temp.getPlayer().X, temp.getPlayer().Y, x, y);
        //                        //temp.getPlayer().revertPosition();
        //                    }
        //                } else
        //                {
        //                    Console.WriteLine("Player position at [{0}, {1}] is valid...", temp.getPlayer().X, temp.getPlayer().Y);
        //                }
        //            }
        //        }
        //    }

        //}

        
        public void ValidatePositions()
        {

            for (int x = 0; x < width;  x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (this.grid[x,y].isOccupied)
                    {
                        PlayerTile temp = (PlayerTile) this.grid[x, y];
                        // checks to make sure object is in correct space 
                        if (x != temp.getPlayer().X || y != temp.getPlayer().Y)
                        {
                            Console.WriteLine("{0}'s position is not valid...", temp.getPlayer().Name);
                            Console.WriteLine("Player current position is [{0}, {1}]", x, y);
                            Console.WriteLine("Player position should be [{0}, {1}]", temp.getPlayer().X, temp.getPlayer().Y);

                            // debug
                            if ((grid[x ,y] as PlayerTile).getPlayer() != null)
                            {
                                Console.WriteLine("Player detected at location: [{0}, {1}]", x, y);
                            }


                            // if prospected position of temp has no player
                            if ((grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile) == null)
                            {
                                Console.WriteLine("Prospected Position of [{0},{1}] does not contain player...", temp.getPlayer().X, temp.getPlayer().Y);
                                Console.WriteLine("Resetting position to new location");
                                this.ResetPlayerPosition(temp.getPlayer(), x, y);   
                            } 

                            // 
                            else
                            {

                                //Console.WriteLine("Collision occured. Fight should begin here...");

                                Console.WriteLine("Potential collision detected.. Printing data...");
                                Console.WriteLine("\tPlayers involved: ");

                                // attacking player
                                Console.WriteLine("Attacking player data");
                                PrintDataAtLoc(x, y);

                                Console.WriteLine("\nDefending player data");
                                PrintDataAtLoc(temp.getPlayer().X, temp.getPlayer().Y);

                                // unused
                                //int defendingPlayerPower = (grid[x, y] as PlayerTile).getPlayer().Power;
                                //int attackingPlayerPower = (grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile).getPlayer().Power;

                                // sets position of defender depending on fight outcome
                                //Console.WriteLine("Setting grid [{0}, {1}] to winner...", temp.getPlayer().X, temp.getPlayer().Y);

                                //Console.WriteLine("Player {0} is fighting player {1}...", temp.getPlayer(), (grid[x,y] as PlayerTile).getPlayer());

                                Console.WriteLine("Player {0} is fighting player {1}...", temp.getPlayer().Name, (grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile).getPlayer().Name);

                                HighlanderMovements.Highlander winner = (fight(temp.getPlayer(), (grid[temp.getPlayer().X, temp.getPlayer().Y] as PlayerTile).getPlayer()));
                                
                                // if winner is attacker
                                if (winner.Equals(temp.getPlayer()))
                                {
                                    ClearCoordinate(temp.getPlayer().X, temp.getPlayer().Y);
                                } 
                                // winner is defender
                                else
                                {
                                    ClearCoordinate(x, y);
                                }
                                

                                //Console.WriteLine("Fight not implemented: temporary operation -> reverting position from prospect [{0}, {1}] -> [{2}, {3}]", temp.getPlayer().X, temp.getPlayer().Y, x, y);
                                //temp.getPlayer().revertPosition();
                            }
                        } else
                        {
                            Console.WriteLine(temp.getPlayer().Name + " position at [{0}, {1}] is valid...", temp.getPlayer().X, temp.getPlayer().Y);
                        }
                    }
                }
            }
        }

        private HighlanderMovements.Highlander fight(HighlanderMovements.Highlander attacker, HighlanderMovements.Highlander defender)
        {

            Console.WriteLine("fight method...\n");
            Console.WriteLine("\tAttacker: {0}\n\tDefender: {1}", attacker.Name, defender.Name);

            Console.WriteLine("Fight begin!");
            if (attacker.Power > defender.Power) {
                Console.WriteLine("Attacker won!");
                Console.WriteLine("Clearing coordinate of player: " + defender.Name);
                //ClearCoordinate(defender.X, defender.Y);
                return attacker;

            } else if (attacker.Power < defender.Power)
            {
                Console.WriteLine("Defender won!");
                Console.WriteLine("Clearing coordinate of player: " + attacker.Name);
                //ClearCoordinate(attacker.X, attacker.Y);
                return defender;
            } else
            {
                // winner decided by random chance 
                int chance = rand.Next(0,2);
                
                if (chance == 0)
                {
                    Console.WriteLine("Attacker won!");
                    Console.WriteLine("\nDEBUG: printing grid\n");
                    //PrintGrid();


                    Console.WriteLine("Clearing coordinate of player: " + defender.Name);
                    //ClearCoordinate(defender.X, defender.Y);
                    return attacker;
                }
                else
                {
                    Console.WriteLine("Defender won!");
                    Console.WriteLine("Clearing coordinate of player: " + attacker.Name);
                    //ClearCoordinate(attacker.X, attacker.Y);
                    return defender;
                }

            }

        }

        private void PrintDataAtLoc(int x, int y)
        {
            
            HighlanderMovements.Highlander h = (grid[x, y] as PlayerTile).getPlayer();
            
            if (h != null)
            {
                Console.WriteLine("\tName: {0}", h.Name);
                Console.WriteLine("\tCurrent Position: [{0}, {1}]", x, y);
                Console.WriteLine("\tProspected Position: [{0}, {1}]", h.X, h.Y);
            }

        }

        private void ResetPlayerPosition(HighlanderMovements.Highlander player, int xPos, int yPos)
        {

            ClearCoordinate(xPos, yPos);
            PlacePlayer(player);
            
        }

        public void PrintGrid()
        {

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Console.WriteLine("attempting to print .getStatus() at [{0},{1}]...", x, y);
                    Console.Write("[{0}], ", this.grid[x,y].getStatus());
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n"); // adds some empty space at end 

        }

        public void ClearCoordinate(int x, int y)
        {
            Console.WriteLine("Resetting coordinate [{0}, {1}]", x, y);

            grid[x,y].setStatus("empty");
            grid[x,y].isOccupied = false;
            (grid[x, y] as PlayerTile).setOccupant(null);

            //(grid[x, y] as PlayerTile). = null;

            Console.WriteLine("grid[{0}, {1}] -> set status is: {2}", x, y, grid[x,y].getStatus());
        }

        public void PlayerListPrint()
        {
            foreach (Tile i in grid)
            {
                if (i.isOccupied)
                {
                    Console.WriteLine("\nPlayer found at position [{0}, {1}]...", i.GetX(), i.GetY());
                    Console.WriteLine("\tPlayer Data:");

                    Console.WriteLine("\t\tName: {0}", (i as PlayerTile).getPlayer().Name);
                    Console.WriteLine("\t\tExpected Position: [{0}, {1}]", (i as PlayerTile).getPlayer().X, (i as PlayerTile).getPlayer().Y);
                    Console.WriteLine("\t\tPlayer Affinity: " + (((i as PlayerTile).getPlayer().IsGood) ? "Good" : "Evil"));
                }
            }
        }


        public int[] getRandomXY()
        {
            int[] ret = new int[2];

            ret[0] = rand.Next(0, width);
            ret[1] = rand.Next(0, height);

            return ret;
        }
    }
}
