using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Explosion : BaseObject
    {
        public int Lifetime { get; set; }
        public Explosion(Point pos, Point dir, Size size, int lifetime) : base(pos, dir, size)
        {
            Lifetime = lifetime; 

            nameFile = GetNameFile("explosion");

            NumberFile = 0;
        }
        public Explosion()
        {
            Lifetime = 0;

            nameFile = GetNameFile("emptiness");

            NumberFile = 0;
        }

        public override void Update()
        {
            Lifetime--; 
        }
    }
}
