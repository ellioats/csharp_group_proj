using GroupProjectCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
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

                    if (highlanderi.IsAlive && !highlanderi.IsGood)
                    {
                        Console.WriteLine($"Bad Highlander {i} Position: ({highlanderiPosition.Item1}, {highlanderiPosition.Item2})");
                    }
                    else
                    {
                        Console.WriteLine($"Good Highlander {i} Position: ({highlanderiPosition.Item1}, {highlanderiPosition.Item2})");
                    }
                }

                // Add a delay for better readability
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("All bad Highlanders defeated!");
        }
    }
}
