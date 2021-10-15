using Asteroids.Scenes;
using System;
using System.Drawing;

namespace Asteroids
{
    class Ship : BaseObject
    {

        public int Energy { get; set; } = 100;
        private int lastDamage = 0;

        public int LastDamage { get { return lastDamage; } }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("ship");

            NumberFile = 0;
        } 

        public void EnergyLow(int damage)
        {
            lastDamage = damage;

            Energy = damage > 0 ? (damage > Energy ? 0 : Energy - damage) : (Energy+Math.Abs(damage) > 100 ? 100 : Energy - damage);
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y -= Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height - Size.Height) Pos.Y += Dir.Y;
        }

        public override void Update()
        {
        }
    }
}
