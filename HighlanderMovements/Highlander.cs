using System;
using System.Collections.Generic;
using System.Linq;
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

        public Highlander(int startX, int startY, bool isGood)
        {
            X = startX;
            Y = startY;

            prevX = startX;
            prevY = startY;

            

            IsGood = isGood;
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
    }
}
