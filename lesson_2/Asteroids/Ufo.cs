using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ufo : BaseObject
    {
        public Ufo(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("ufo");

            NumberFile = 3;
        }

        public override void Update()
        {
            Pos.X += Dir.X;

            if (Pos.X >= Game.Width) Pos.X = 0;
        }
    }
}
