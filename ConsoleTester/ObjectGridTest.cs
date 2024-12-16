using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Game;
using HighlanderMovements;

namespace ConsoleTester
{
    class ObjectGridTest
    {
        public static void gridPrintTest()
        {
            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);


            grid.PrintGrid();
            
        }

        // goal: to have grid print and show player on grid
        public static void gridPrintTestWithPlayer()
        {
            Highlander player = new Highlander(5,5,true, "test");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);
            grid.PlacePlayer(player);
            grid.PrintGrid();

        }

        
        public static void gridPlayerMoveTest()
        {
            Highlander player = new Highlander(5, 5, true, "test");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(player);

            Console.WriteLine("Print before move");
            grid.PrintGrid();

            player.MoveLeft();

            grid.ValidatePositions();

            Console.WriteLine("Print after move");
            grid.PrintGrid();

            Console.WriteLine("Second validate call (good data unit test)...");
            grid.ValidatePositions();
        }

        public static void gridPlayerRandomMoveTest()
        {
            Highlander player = new Highlander(5, 5, true, "test");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(player);

            for (int x = 0; x < 5; x++)
            {
                player.MoveRandomly(width, height);

                grid.ValidatePositions();

                Console.WriteLine("Grid print at iteration : " + x);
                grid.PrintGrid();

            }


            Console.WriteLine("End");
        }

        // WIP - Collision detection is currently not functional
        public static void twoPlayerCollisionTestUnMoving()
        {

            Highlander player1 = new Highlander(1,1, true, "Wowie Zowie");
            Highlander player2 = new Highlander(0, 1, false, "Womper chomper");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(player1);
            grid.PlacePlayer(player2);

            grid.ValidatePositions();

            grid.PrintGrid();
            grid.PlayerListPrint();

            Console.WriteLine("IN MAIN ROUTINE: player1.MoveLeft() called...");
            player1.MoveLeft();

            grid.ValidatePositions();

            Console.WriteLine("Grid after expected collision");
            grid.PrintGrid();
            grid.PlayerListPrint();

            Console.WriteLine("End...");
        }
        
        public static void GridPlayerPrintTest()
        {

            Highlander player1 = new Highlander(1,1, true, "");
            Highlander player2 = new Highlander(5, 6, true, "");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(player1);
            grid.PlacePlayer(player2);

            grid.ValidatePositions();

            grid.PlayerListPrint();

        }

        public static void GridPlayerRemoveTest()
        {
            Highlander p = new Highlander(0, 0, true, "silly");


            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(p);

            grid.ValidatePositions();

            Console.WriteLine("First print");
            grid.PlayerListPrint();

            grid.ClearCoordinate(0,0);

            Console.WriteLine("Second print");
            grid.PlayerListPrint();

        }

        public static void PlayerFightTest2()
        {
            Highlander player1 = new Highlander(1,1, true, "");
            Highlander player2 = new Highlander(5, 6, true, "");

            const int width = 10;
            const int height = 10;

            ObjectGrid grid = new ObjectGrid(width, height);

            grid.PlacePlayer(player1);
            grid.PlacePlayer(player2);

            grid.ValidatePositions();

        }


        // scripted routine test
        public static void RoutineTest2Player()
        {
            ObjectGrid g = new ObjectGrid(10, 10);

            int[] p1Coords = g.getRandomXY(); 
            int[] p2Coords = g.getRandomXY(); 

            Highlander p1 = new Highlander(p1Coords[0], p1Coords[1], true, "P1");
            Highlander p2 = new Highlander(p2Coords[0], p2Coords[1], true, "P2");

            g.PlacePlayer(p1);
            g.PlacePlayer(p2);

            g.ValidatePositions();

            Console.WriteLine("Grid print before loop..");
            g.PrintGrid();
            Console.WriteLine("\nPlayer list print before loop...");
            g.PlayerListPrint();

            // game loop start 
            for (int x = 0; x < 10; x++)
            {
                p1.MoveRandomly(10, 10);

                g.ValidatePositions();

                p1.MoveRandomly(10, 10);

                g.ValidatePositions();

                Console.WriteLine("Iteration {0} grid print...");
                g.PrintGrid();
            }
        }


    }
}
