using System.Drawing;

namespace Simulator
{
    public enum MoveDirections
    {
        Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight
    }

    public sealed class PersonMoveEventArgs : EventArgs
    {
        public Point PreviousPosition { get; set; }
        public Point CurrentPosition { get; set; }
    }

    public sealed class Person(Point position, float resistance)
    {
        public event EventHandler? OnInfection;
        public event EventHandler? OnContagious;
        public event EventHandler? OnCured;
        public event EventHandler? OnDeath;
        public event EventHandler<PersonMoveEventArgs>? OnMoved;

        public Point Position { get; set; } = position;
        public float Resistance { get; set; } = resistance;
        public float Health { get; set; } = 100;
        public bool IsInfected { get; set; }
        public bool IsContagious { get; set; }
        public byte IncubationTime { get; set; }
        public byte DaysOfImmunity { get; set; }
        public byte DmgDelay { get; set; }
        public byte ContagiousTime { get; set; }

        public bool CanMove => Health > 0.5;

        public void Infect()
        {
            IsInfected = true;
            OnInfection?.Invoke(this, EventArgs.Empty);
        }

        public void MakeContagious()
        {
            IsContagious = true;
            OnContagious?.Invoke(this, EventArgs.Empty);
        }

        public void Cure()
        {
            IsInfected = false;
            IsContagious = false;
            OnCured?.Invoke(this, EventArgs.Empty);
        }

        public void Die()
        {
            Health = 0;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }

        public void Move(Point newPosition)
        {
            OnMoved?.Invoke(this, new PersonMoveEventArgs
            {
                PreviousPosition = Position,
                CurrentPosition = newPosition
            });
            Position = newPosition;
        }
    }
}