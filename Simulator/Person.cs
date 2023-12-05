using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public enum MoveDirections
    {
        Left, Right, Up, Down
    }

    public class Person
    {
        static int halfHealth; // Micro optimization
        public Point Position { get; set; }
        public float Vulnerability { get; set; }
        public int Health { get; set; }
        public bool IsInfected { get; set; }
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
            MoveDirections direction = (MoveDirections)rnd.Next(0, 4);
            
        }

        public void HaveANiceDay()
        {
            if(Health > halfHealth)
                Move();
        }
    }
}
