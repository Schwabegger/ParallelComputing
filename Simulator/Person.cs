using System.Drawing;

namespace Simulator
{
    public enum MoveDirections
    {
        Left, Right, Up, Down
    }

    public sealed class PersonMoveEventArgs : EventArgs
    {
        public Point PreviousPosition { get; set; }
        public Point CurrentPosition { get; set; }
    }

    public sealed class Person
    {
        static int halfHealth; // Micro optimization
        public Point Position { get; set; }
        public float Vulnerability { get; set; }
        public int Health { get; set; }
        public bool IsInfected { get; set; }
        public bool IsContagious { get; private set; }
        public byte IncubationTime { get; set; }
        public byte TimeSinceInfection { get; private set; }
        public bool IsAlive { get; private set;}

        public event EventHandler? OnInfection;
        public event EventHandler? OnContagious;
        public event EventHandler? OnHealed;
        public event EventHandler? OnDeath;
        public event EventHandler<PersonMoveEventArgs>? OnMoved;

        Random rnd = new Random();

        public Person(int health, float vulnerability, Point position)
        {
            halfHealth = health / 2;
            Health = health;
            Vulnerability = vulnerability;
            Position = position;
        }

        public void Move()
        {
            var previousPosition = Position;
            MoveDirections direction = (MoveDirections)rnd.Next(0, 4);
            _ = direction switch
            {
                MoveDirections.Left => Position = new Point(Position.X - 1, Position.Y),
                MoveDirections.Right => Position = new Point(Position.X + 1, Position.Y),
                MoveDirections.Up => Position = new Point(Position.X, Position.Y - 1),
                MoveDirections.Down => Position = new Point(Position.X, Position.Y + 1),
                _ => throw new NotImplementedException(),
            };
            OnMoved?.Invoke(this, new PersonMoveEventArgs { PreviousPosition = previousPosition, CurrentPosition = Position });
        }

        public void HaveANiceDay()
        {
            if (Health > halfHealth)
                Move();

            if (IsContagious)
            {

            }
            else if (IsInfected)
            {
                TimeSinceInfection++;
                if(TimeSinceInfection > IncubationTime && !IsContagious)
                {
                    IsContagious = true;
                    OnContagious?.Invoke(this, EventArgs.Empty);
                }
            }

            // If health is 0 or less, the person is dead
            if (Health <= 0)
            {
                IsInfected = false;
                IsContagious = false;
                IsAlive = false;

                // Throw an event to notify the world that this person is dead
                OnDeath?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}