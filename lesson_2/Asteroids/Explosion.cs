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
        public Explosion(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("explosion");

            NumberFile = 0;
        }
        public Explosion()
        {
            nameFile = GetNameFile("emptiness");

            NumberFile = 0;
        }
    }
}
