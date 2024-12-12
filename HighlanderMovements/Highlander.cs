using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HighlanderMovements
{
    public class Highlander
    {
        // constants 
        const int _health = 100;
        const int _power = 5;
        const int _powerIncrement = 5;

        // instance variables, highlander traits 
        public int Health { get; private set; } = _health; // default health
        public int Power { get; private set; } = _power; // default value, can be changed
        public bool IsGood { get; private set; }
        public bool IsAlive => Health > 0;

        public int X { get; private set; }
        public int Y { get; private set; }

        public int prevX { get; private set; }
        public int prevY { get; private set; }

        private Boolean setPrevCoordPace = false;
        private Random rand;

        public Highlander(int startX, int startY, bool isGood)
        {
            X = startX;
            Y = startY;

            prevX = startX;
            prevY = startY;

            

            IsGood = isGood;

            rand = new Random();
        }

        public Highlander(int[] startCoords, bool isGood)
        {
            X = startCoords[0];
            Y = startCoords[1];
            this.IsGood = isGood;
        }

        // when a highlander kills another highlander, run this 
        public void HighlanderKill()
        {
            this.Power += _powerIncrement;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        
        private void OnMove()
        {
            if (this.setPrevCoordPace)
            {
                this.prevX = this.X;
                this.prevY = this.Y;
            }

        }

        public void MoveRandomly(int maxWidth, int maxHeight)
        {
            int direction = rand.Next(0, 8);

            switch (direction) {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveDownRight();
                    break;
                case 2:
                    MoveDown();
                    break;
                case 3:
                    MoveDownLeft();
                    break;
                case 4:
                    MoveLeft();
                    break;
                case 5:
                    MoveUpLeft();
                    break;
                case 6:
                    MoveUp();
                    break;
                case 7:
                    MoveUpRight();
                    break;
            }

            //Console.WriteLine("validating position [{0},{1}]", X, Y);
            this.validateNewPosition(maxWidth, maxHeight);

        }

        private void validateNewPosition(int maxWidth, int maxHeight)
        {
            // revert to prevX or prevY values
            if (this.X == maxWidth || this.X == -1)
            {
                //Console.WriteLine("{0} is equal to {1}(maxWidth)", X, maxWidth);
                this.X = (X < 0) ? 1 : maxWidth - 1 ;
                //this.MoveRandomly(maxWidth, maxHeight);
            } else if (this.Y == maxHeight || this.Y == -1)
            {
                //Console.WriteLine("{0} is equal to {1}(maxHeight)", Y, maxHeight);
                this.Y = (Y < 0) ? 1 : maxHeight - 1 ;
                //this.MoveRandomly(maxWidth, maxHeight);
            }

            //Console.WriteLine("Returning...");
            return;


        }

        public void MoveUp() {
            OnMove();
            Y++;
            this.setPrevCoordPace = true;
        }
        public void MoveDown() { 
            OnMove();
            Y--; 
            this.setPrevCoordPace = true;
        }
        public void MoveLeft() { 
            OnMove();
            X--;
            this.setPrevCoordPace = true;
        }

        public void MoveRight() {
            OnMove();
            X++; 
            this.setPrevCoordPace = true;
        }
        public void MoveDownRight() { 
            OnMove();
            X++; Y--; 
            this.setPrevCoordPace = true;
        }
        public void MoveDownLeft() {
            OnMove();
            X--; Y--; 
            this.setPrevCoordPace = true;
        }
        public void MoveUpRight() { 
            OnMove();
            X++; Y++;
            this.setPrevCoordPace = true;
        }
        public void MoveUpLeft() { 
            OnMove();
            X--; Y++;
            this.setPrevCoordPace = true;
        }


        // routine to run every "turn"
        public void routine(string[,] area)
        {

            List<HighlanderInfo> otherHighlanders = findAllNearbyHighlanders(area, this.X, this.Y);

            // depending on the enemies around and their affinity. Move away or towards. 


        }


        private void moveTowards(string[,] area, HighlanderInfo info)
        {

            if (info.xPos > this.X)
            {
                if (info.yPos > this.Y)
                {
                    MoveUpRight();
                } else if (info.yPos < this.Y)
                {
                    MoveDownRight();
                } else
                {
                    MoveRight();
                }
            } else if (info.xPos < this.X)
            {
                if (info.yPos > this.Y)
                {
                    MoveUpLeft();
                } else if (info.yPos < this.Y)
                {
                    MoveDownLeft();
                } else
                {
                    MoveLeft();
                }
            } else
            {
                if (info.yPos > this.Y)
                {
                    MoveUp();
                } else if (info.yPos < this.Y)
                {
                    MoveDown();
                } else
                {
                    // start collision??
                }
                
            }

            this.validateNewPosition(area.GetLength(0), area.GetLength(1));


            
        }


        private List<HighlanderInfo> findAllNearbyHighlanders(string[,] area, int playerCurrentX, int playerCurrentY)
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


                    if (area[x, y] == "Evil")
                    {
                        HighlanderInfo a = new HighlanderInfo();
                        a.yPos = y;
                        a.xPos = x;
                        a.isEvil = true;
                        info.Add(a);
                    }
                    else if (area[x, y] == "Good")
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
    }
}
