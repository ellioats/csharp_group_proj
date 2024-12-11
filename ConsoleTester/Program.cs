using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighlanderMovements;
using Game;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //test1();
            //HighlanderMovementsTest1();
            //RandomMovementTest();
            gridPlayerMovementTest();


            Console.Read();
        }

        static Grid gridInit(int x, int y)
        {
            return new Grid(x,y);
        }


        static void printArea(string[][] area)
        {
            Console.WriteLine("Area print -- ");
            for (int x = 0; x < area.GetLength(0); x++)
            {
                for (int y = 0; y < area[x].GetLength(0); y++)
                {
                    Console.Write("{0}, ", area[x][y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
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
                new string[] {"", "8", "Good", "6", "", ""},
                new string[] {"", "1", "P", "5", "", ""},
                new string[] {"", "2", "3", "Evil", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "Good", "", "", ""},
                new string[] {"", "", "", "", "", ""}
            };

            int CurrentPlayerX = 1;
            int CurrentPlayerY = 2;
            
            List<HighlanderMovements.HighlanderInfo> info = HighlanderMovements.Class1.findAllNearbyHighlanders(ref area, CurrentPlayerX, CurrentPlayerY);


            if (info.Count > 0)
            {
                foreach (HighlanderMovements.HighlanderInfo i in info)
                {
                    Console.WriteLine("{0} Highlander found at position {1}, {2}", (i.isEvil ? "Evil" : "Good"), i.xPos, i.yPos);
                }
            } else
            {
                Console.WriteLine("List is null...");
            }

        }

        static void RandomMovementTest()
        {

            string[][] area = {
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "Good", "", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "", "", "", ""},
                new string[] {"", "", "", "", "", ""}
            };

            printArea(area);

            HighlanderMovements.Highlander highlander = new HighlanderMovements.Highlander(1, 2, true);

            HighlanderMovements.Class1.MoveRandomly(ref area, ref highlander);

            printArea(area);

        }


        /*
         GRID.CS TESTS
         */

        static void gridPlayerMovementTest()
        {
            Grid g = new Grid(20,20);
            g.initGrid();

            
            Highlander h1 = new Highlander(5,5, true);

            // player is placed at position 5,5
            g.placePlayer(h1);

            Console.WriteLine("First Print: ");
            g.printGrid();

            // new position should be 4,5
            h1.MoveRight();

            // update to reflect position changes
            g.gridUpdate();


            Console.WriteLine("Second Print: ");
            g.printGrid();

            Console.WriteLine("FINAL INFO: ");
            Console.WriteLine("Total num of players: {0}", g.currentNumOfPlayers());
                


        }

    }
}
