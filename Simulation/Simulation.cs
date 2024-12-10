using GroupProjectCSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
    public class Simulation
    {
        private Highlander player;
        private List<Highlander> highlanders;
        private Random random;
        private int width;
        private int height;

        public Simulation(int playerStartX, int playerStartY, int width, int height)
        {
            player = new Highlander(playerStartX, playerStartY, true);
            highlanders = new List<Highlander>();
            random = new Random();
            this.width = width;
            this.height = height;
        }

        public void AddHighlander(int startX, int startY, bool isGood)
        {
            highlanders.Add(new Highlander(startX, startY, isGood));
        }

        public void MovePlayerRandomly()
        {
            int direction = random.Next(4);

            switch (direction)
            {
                case 0:
                    if (player.Y < height - 1) player.MoveUp();
                    break;
                case 1:
                    if (player.Y > 0) player.MoveDown();
                    break;
                case 2:
                    if (player.X > 0) player.MoveLeft();
                    break;
                case 3:
                    if (player.X < width - 1) player.MoveRight();
                    break;
                default:
                    throw new ArgumentException("Invalid direction");
            }

            CheckForCombat();
        }

        private void CheckForCombat()
        {
            foreach (var highlander in highlanders.Where(h => h.IsAlive && !h.IsGood))
            {
                if (highlander.X == player.X && highlander.Y == player.Y)
                {
                    highlander.TakeDamage(player.Power);
                    Console.WriteLine("Player attacked a bad Highlander!");

                    if (!highlander.IsAlive)
                    {
                        Console.WriteLine("A bad Highlander has been defeated!");
                        player.HighlanderKill();
                    }
                }
            }
        }

        public void MoveHighlanderRandomly(int index)
        {
            if (index < 0 || index >= highlanders.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var highlander = highlanders[index];

            if (highlander.IsGood && highlander.IsAlive)
            {
                int direction = random.Next(4);

                switch (direction)
                {
                    case 0:
                        if (highlander.Y < height - 1) highlander.MoveUp();
                        break;
                    case 1:
                        if (highlander.Y > 0) highlander.MoveDown();
                        break;
                    case 2:
                        if (highlander.X > 0) highlander.MoveLeft();
                        break;
                    case 3:
                        if (highlander.X < width - 1) highlander.MoveRight();
                        break;
                    default:
                        throw new ArgumentException("Invalid direction");
                }
            }
        }

        public (int, int) GetPlayerPosition()
        {
            return (player.X, player.Y);
        }

        public (int, int) GetHighlanderPosition(int index)
        {
            if (index < 0 || index >= highlanders.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var highlander = highlanders[index];

            return (highlander.X, highlander.Y);
        }

        public bool AreAllBadHighlandersDefeated()
        {
            return highlanders.All(h => h.IsGood || !h.IsAlive);
        }

        public int HighlandersCount => highlanders.Count(h => h.IsAlive);
    }
}