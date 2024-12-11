using GroupProjectCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighlanderMovements;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            test1();
            HighlanderMovementsTest1();
        }

        static void test1()
        {
            int width = 10;
            int height = 10;

            Simulation.Simulation simulation = new Simulation.Simulation(0, 0, width, height);

            // Add some good and bad Highlanders
            simulation.AddHighlander(1, 1, true); // Good Highlander
            simulation.AddHighlander(2, 2, false); // Bad Highlander
            simulation.AddHighlander(3, 3, false); // Bad Highlander

            // Game loop
            while (!simulation.AreAllBadHighlandersDefeated())
            {
                // Move the player randomly
                simulation.MovePlayerRandomly();

                // Move each good Highlander randomly
                for (int i = 0; i < simulation.HighlandersCount; i++)
                {
                    simulation.MoveHighlanderRandomly(i);
                }

                // Check positions
                var playerPosition = simulation.GetPlayerPosition();
                Console.WriteLine($"Player Position: ({playerPosition.Item1}, {playerPosition.Item2})");

                for (int i = 0; i < simulation.HighlandersCount; i++)
                {
                    var highlanderPosition = simulation.GetHighlanderPosition(i);
                    var highlander = simulation.GetHighlander(i);

                    if (highlander.IsAlive && !highlander.IsGood)
                    {
                        Console.WriteLine($"Bad Highlander {i} Position: ({highlanderPosition.Item1}, {highlanderPosition.Item2})");
                    }
                    else
                    {
                        Console.WriteLine($"Good Highlander {i} Position: ({highlanderPosition.Item1}, {highlanderPosition.Item2})");
                    }
                }

                // Add a delay for better readability
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("All bad Highlanders defeated!");
        }

        static void HighlanderMovementsTest1()
        {
            string[][] area = {
                new string[] {"", "8", "7", "6", "", ""},
                new string[] {"", "1", "P", "5", "", ""},
                new string[] {"", "2", "3", "4", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "", "", "", ""}
            };

            int CurrentPlayerX = 1;
            int CurrentPlayerY = 2;

            List<HighlanderMovements.HighlanderInfo> info = HighlanderMovements.Class1.findAllNearbyHighlanders(ref area, CurrentPlayerX, CurrentPlayerY);

            if (info != null)
            {
                foreach (HighlanderMovements.HighlanderInfo i in info)
                {
                    Console.WriteLine("Highlander found at position {0}, {1}", i.xPos, i.yPos);
                }
            }
        }
    }
}
