namespace GroupProjectCSharp
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

        public Highlander(int startX, int startY, bool isGood)
        {
            X = startX;
            Y = startY;
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

        public void MoveUp() => Y++;
        public void MoveDown() => Y--;
        public void MoveLeft() => X--;
        public void MoveRight() => X++;
    }
}