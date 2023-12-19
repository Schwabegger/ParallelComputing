﻿using System.Drawing;

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

        private readonly PersonMoveEventArgs _moveEventArgs = new();

        public Point Position { get; set; } = position;
        public float Resistance { get; set; } = resistance;
        public float AdditionalInfectionResistance { get; set; }
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
            _moveEventArgs.PreviousPosition = Position;
            _moveEventArgs.CurrentPosition = newPosition;
            OnMoved?.Invoke(this, _moveEventArgs);
            Position = newPosition;
        }

        public bool IsSusceptibleToInfection()
        {
            return !IsInfected && !IsContagious && DaysOfImmunity <= 0;
        }
    }
}