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
    class Star : BaseObject
    {
        public Star(Point pos, Point dir, Size size) : base (pos, dir, size)
        {
            nameFile = GetNameFile("star");

            NumberFile = random.Next(0, nameFile.Length - 1);
        }
    }
}
