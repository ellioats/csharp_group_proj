using System;
using HighlanderDB;

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
        public int health { get; private set; } = _health;
        public int power { get; private set; } = _power;
        public bool IsGood { get; private set; }
        public int age { get; private set; }
        public string name { get; private set; }
        public int kills { get; private set; }

        public bool IsAlive => health > 0;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int prevX { get; private set; }
        public int prevY { get; private set; }

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
            prevY = startY;

            IsGood = isGood;
            age = GenerateRandomAge();
            name = GenerateRandomName();
            power = (_power + (age / 10));
        }

        public Highlander(int[] startCoords, bool isGood)
        {
            X = startCoords[0];
            Y = startCoords[1];
            prevX = startCoords[0];
            prevY = startCoords[1];

            IsGood = isGood;
            age = GenerateRandomAge();
            name = GenerateRandomName();
            power = (_power + (age / 10));
        }

        // Combat methods
        public void HighlanderKill()
        {
            power += _powerIncrement;
            kills++;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0) health = 0;
        }

        public static bool fight(Highlander killer, Highlander victim, DatabaseManager dbManager)
        {
            Random random = new Random();

            while (killer.IsAlive && victim.IsAlive)
            {
                int killerAttack = random.Next(1, 11) * killer.power;
                int victimAttack = random.Next(1, 11) * victim.power;

                victim.TakeDamage(killerAttack);
                killer.TakeDamage(victimAttack);

                Console.WriteLine($"{killer.name} attacks {victim.name} with {killerAttack} damage. {victim.name} health: {victim.health}");
                Console.WriteLine($"{victim.name} attacks {killer.name} with {victimAttack} damage. {killer.name} health: {killer.health}");
                Console.WriteLine("-----------------------");
            }

            // Determine winner
            if (killer.IsAlive)
            {
                Console.WriteLine($"{killer.name} wins!");
                killer.HighlanderKill();

                dbManager.RecordKill(killer.ID, victim.ID); // Log the kill in the database
                return true;
            }
            else
            {
                Console.WriteLine($"{victim.name} wins!");
                victim.HighlanderKill();
                dbManager.RecordKill(victim.ID, killer.ID); // Log the kill in the database
                return false;
            }
        }


        // Movement methods
        private void OnMove()
        {
            if (setPrevCoordPace)
            {
                prevX = X;
                prevY = Y;
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
    }
}
