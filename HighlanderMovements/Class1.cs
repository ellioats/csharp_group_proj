using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HighlanderMovements
{
    public struct HighlanderInfo
    {
        public Boolean isEvil;
        public int xPos;
        public int yPos;
    }

    public class Class1
    {


        const int SQUARESTOCHECK = 8;

        /*
         * Comments
         * 
         * 
         */

        /// <summary>
        ///  Checks 1 square out from the player's current position, 
        /// </summary>
        /// <param name="area">Playing Field</param>
        /// <param name="isEvil">Sets value to true if an evil highlander is found, false if a good highlander is near, null if no highlander is found</param>
        public static void isHighlanderNear(ref string[][] area, int playerCurrentX, int playerCurrentY, ref Boolean? isEvil, ref string outputText)
        {
            isEvil = null;

            // checking position starts in top left corner of 3x3 grid
            int checkingCurrentPosX = playerCurrentX - 1;
            int checkingCurrentPosY = playerCurrentY - 1;

            int checkingBoundX = checkingCurrentPosX + 2;
            int checkingBoundY = checkingCurrentPosY + 2;

            for (int x = checkingCurrentPosX; x < (checkingBoundX + 1); x++)
            {
                for (int y = checkingCurrentPosY; y < (checkingBoundY + 1); y++)
                {

                    if (x < 0 && y < 0)
                        continue;
                    // disregards player's current position
                    if (x == playerCurrentX && y == playerCurrentY)
                        continue;

                    outputText += "" + area[x][y];

                    if (area[x][y] == "Evil")
                    {
                        outputText += "\nBad Highlander Found";
                        isEvil = true;
                        return;
                    }
                    else if (area[x][y] == "Good")
                    {
                        outputText += "\nGood Highlander Found";
                        isEvil = false;
                        return;
                    }
                    

                }
                
            }


        }
        /// <summary>
        /// Method That returns the location and orientation (good/evil) of any highlander in a 3x3 area around the player
        /// </summary>
        /// <param name="area">the 2d array which represents the area</param>
        /// <param name="playerCurrentX"><Player's current position on the X axis/param>
        /// <param name="playerCurrentY">Player's current position on the Y axis</param>
        /// <returns>A list of the HighLanderInfo struct, which contains coordinate and orientation (good/evil) info</returns>
        public static List<HighlanderInfo> findAllNearbyHighlanders(ref string[][] area, int playerCurrentX, int playerCurrentY)
        {

            int checkingCurrentPosX = playerCurrentX - 1;
            int checkingCurrentPosY = playerCurrentY - 1;

            int checkingBoundX = checkingCurrentPosX + 2;
            int checkingBoundY = checkingCurrentPosY + 2;

            List<HighlanderInfo> info = new List<HighlanderInfo>();
            
            for (int x = checkingCurrentPosX; x < (checkingBoundX + 1); x++)
            {
                for (int y = checkingCurrentPosY; y < (checkingBoundY + 1); y++)
                {

                    if (x < 0 && y < 0)
                        continue;
                    // disregards player's current position
                    if (x == playerCurrentX && y == playerCurrentY)
                        continue;


                    if (area[x][y] == "Evil")
                    {
                        HighlanderInfo a = new HighlanderInfo();
                        a.yPos = y;
                        a.xPos = x;
                        a.isEvil = true;
                        info.Add(a);
                    }
                    else if (area[x][y] == "Good")
                    {
                        HighlanderInfo a = new HighlanderInfo();
                        a.yPos = y;
                        a.xPos = x;
                        a.isEvil = false;
                        info.Add(a);
                    }
                }
            }
                return info;
        }

        // TODO: Code Coverage Unit Test
        public static void MoveRandomly(ref string[][] area, ref HighlanderMovements.Highlander player)
        {
            Random rand = new Random();
            int direction = rand.Next(0, 8);


            switch (direction) {
                case 0:
                    player.MoveRight();
                    break;
                case 1:
                    player.MoveDownRight();
                    break;
                case 2:
                    player.MoveDown();
                    break;
                case 3:
                    player.MoveDownLeft();
                    break;
                case 4:
                    player.MoveLeft();
                    break;
                case 5:
                    player.MoveUpLeft();
                    break;
                case 6:
                    player.MoveUp();
                    break;
                case 7:
                    player.MoveUpRight();
                    break;
            }
            
        }



    }
}
