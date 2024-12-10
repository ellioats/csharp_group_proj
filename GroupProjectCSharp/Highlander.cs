using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectCSharp
{
    class Highlander
    {

        // constants 
        const int _health = 100;
        const int _power = 5;
        const int _powerIncrement = 5;

        // static variables
        int numGoodHighlanders;
        int numBadHighlanders;

        // instance variables, highlander traits 
        int health = _health; // default health
        int power = _power; // default value, can be changed

        // when a highlander kills another highlander, run this 
        public void highlanderKill()
        {
            this.power += _powerIncrement;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Highlander(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }

        public void MoveUp() => Y++;
        public void MoveDown() => Y--;
        public void MoveLeft() => X--;
        public void MoveRight() => X++;

    }
}
