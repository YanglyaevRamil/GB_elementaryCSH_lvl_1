using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) 
        {
            nameFile = GetNameFile("laser");

            NumberFile = random.Next(0, nameFile.Length - 1);
        }

        public override void Update()
        {
            Pos.X += Dir.X;

            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X + Size.Width > Game.Width) Dir.X = -Dir.X;
        }

    }
}
