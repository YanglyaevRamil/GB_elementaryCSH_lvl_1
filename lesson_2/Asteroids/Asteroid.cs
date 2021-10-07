using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("meteorBrown");

            NumberFile = random.Next(0, nameFile.Length - 1);
        }

    }
}
