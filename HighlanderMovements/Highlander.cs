using System;
using System.Collections.Generic;

namespace HighlanderMovements
{
    public class Highlander
    {
        // Constants
        const int _health = 100;
        const int _power = 5;
        const int _powerIncrement = 5;

        // Static Random instance for shared randomness
        private static readonly Random rand = new Random();

        // Random name pool
        public static string[] names = { "Bjorn", "Felix", "Terence", "Elliot", "Hammad", "Leeman", "Russ", "Amanda", "Horus", "Perturabo", "Lupercal", "Arvid", "Scott", "David", "Jacob", "Dexter", "Orm" };

        // Highlander traits
        public int ID { get; set; }
        public int Health { get; private set; } = _health;
        public int Power { get; private set; } = _power;

        public bool IsGood { get; private set; }
        public int Age { get; private set; }
        public string Name { get; private set; }
        public int Kills { get; private set; }

        public bool IsAlive => Health > 0;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int prevX { get; private set; }
        public int PrevY { get; private set; }

        private bool setPrevCoordPace = false;



        // Static Random Generators
        public static int GenerateRandomAge() => rand.Next(18, 101);
        public static string GenerateRandomName() => names[rand.Next(names.Length)];

        // Constructor
        public Highlander(int startX, int startY, bool isGood)
        {
            X = startX;
            Y = startY;
            prevX = startX;
            PrevY = startY;

            IsGood = isGood;
            Age = GenerateRandomAge();
            Name = GenerateRandomName();
            Power = (_power + (Age / 10));
        }

        public Highlander(int[] startCoords, bool isGood)
        {
            X = startCoords[0];
            Y = startCoords[1];

            IsGood = isGood;
            Age = GenerateRandomAge();
            Name = GenerateRandomName();
            Power = (_power + (Age / 10));
        }

        // Combat methods
        public void HighlanderKill()
        {
            Power += _powerIncrement;
            Kills++;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public static bool fight(Highlander killer, Highlander victim)
        {
            Random random = new Random();

            while (killer.IsAlive && victim.IsAlive)
            {
                int killerAttack = random.Next(1, 11) * killer.Power;
                int victimAttack = random.Next(1, 11) * victim.Power;

                victim.TakeDamage(killerAttack);
                killer.TakeDamage(victimAttack);

                Console.WriteLine($"{killer.Name} attacks {victim.Name} with {killerAttack} damage. {victim.Name} health: {victim.Health}");
                Console.WriteLine($"{victim.Name}  attacks  {killer.Name}  with  {victimAttack}  damage.  {killer.Name} health: {killer.Health}");
                Console.WriteLine("-----------------------");
            }

            // Determine winner
            if (killer.IsAlive)
            {
                Console.WriteLine($"{killer.Name} wins!");
                killer.HighlanderKill();

                //dbManager.RecordKill(killer.ID, victim.ID); // Log the kill in the database
                return true;
            }
            else
            {
                Console.WriteLine($"{victim.Name} wins!");
                victim.HighlanderKill();
                //dbManager.RecordKill(victim.ID, killer.ID); // Log the kill in the database
                return false;
            }
        }


        // Movement methods
        private void OnMove()
        {
            if (setPrevCoordPace)
            {
                prevX = X;
                PrevY = Y;
            }
        }

        public void MoveUp() { OnMove(); Y++; setPrevCoordPace = true; }
        public void MoveDown() { OnMove(); Y--; setPrevCoordPace = true; }
        public void MoveLeft() { OnMove(); X--; setPrevCoordPace = true; }
        public void MoveRight() { OnMove(); X++; setPrevCoordPace = true; }
        public void MoveDownRight() { OnMove(); X++; Y--; setPrevCoordPace = true; }
        public void MoveDownLeft() { OnMove(); X--; Y--; setPrevCoordPace = true; }
        public void MoveUpRight() { OnMove(); X++; Y++; setPrevCoordPace = true; }
        public void MoveUpLeft() { OnMove(); X--; Y++; setPrevCoordPace = true; }

        public void MoveRandomly(int maxWidth, int maxHeight)
        {
            int direction = rand.Next(0, 8);

            switch (direction)
            {
                case 0: MoveRight(); break;
                case 1: MoveDownRight(); break;
                case 2: MoveDown(); break;
                case 3: MoveDownLeft(); break;
                case 4: MoveLeft(); break;
                case 5: MoveUpLeft(); break;
                case 6: MoveUp(); break;
                case 7: MoveUpRight(); break;
            }

            ValidateNewPosition(maxWidth, maxHeight);
        }

        private void ValidateNewPosition(int maxWidth, int maxHeight)
        {
            if (X >= maxWidth) X = maxWidth - 1;
            if (X < 0) X = 0;

            if (Y >= maxHeight) Y = maxHeight - 1;
            if (Y < 0) Y = 0;
        }
        public void revertPosition()
        {
            this.X = prevX; 
            this.Y = PrevY;
        }

        //public void MoveUp() {
        //    OnMove();
        //    Y++;
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveDown() { 
        //    OnMove();
        //    Y--; 
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveLeft() { 
        //    OnMove();
        //    X--;
        //    this.setPrevCoordPace = true;
        //}

        //public void MoveRight() {
        //    OnMove();
        //    X++; 
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveDownRight() { 
        //    OnMove();
        //    X++; Y--; 
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveDownLeft() {
        //    OnMove();
        //    X--; Y--; 
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveUpRight() { 
        //    OnMove();
        //    X++; Y++;
        //    this.setPrevCoordPace = true;
        //}
        //public void MoveUpLeft() { 
        //    OnMove();
        //    X--; Y++;
        //    this.setPrevCoordPace = true;
        //}

        // routine to run every "turn"
        public void routine(string[,] area)
        {
            
            

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

            this.ValidateNewPosition(area.GetLength(0), area.GetLength(1));


            
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

                    if (x < 0 && y < 0 || y >= area.GetLength(1) || x >= area.GetLength(0) || y < 0|| x < 0)
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

        //public static bool fight(Highlander p1, Highlander p2)
        //{
        //    Console.WriteLine("Fight!");
        //    // return result: true, p1 won, 
        //    // false, p2 won

        //    Random rand = new Random();

        //    if (p1.Power > p2.Power)
        //    {
        //        Console.WriteLine("P1 wins!");
        //        p1.HighlanderKill();
        //        return true;
        //    }
        //    else if (p1.Power < p2.Power)
        //    {
        //        Console.WriteLine("P2 wins!");
        //        p2.HighlanderKill();
        //        return false;   
        //    } 
        //    else
        //    {
        //        int result = rand.Next(0,2);
        //        if (result == 0)
        //        {
        //            Console.WriteLine("P2 wins!");
        //            p2.HighlanderKill();
        //            return false;
        //        }
        //        else
        //        {
        //            Console.WriteLine("P1 wins!");
        //            p1.HighlanderKill();
        //            return true;
        //        }
        //    }
        //}
        
    }
}
