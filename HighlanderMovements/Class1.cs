using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HighlanderMovements
{
    public class Class1
    {

        private struct HighlanderInfo
        {
            public Boolean isEvil;
            public int xPos;
            public int yPos;
        }

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

        public static HighlanderInfo[] findAllNearbyHighlanders(ref string[][] area, int playerCurrentX, int playerCurrentY)
        {

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


                    if (area[x][y] == "Evil")
                    {

                    }
                    else if (area[x][y] == "Good")
                    {
                    }
                    

                }
                
            }

        }

    }
}
