using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Satellite : BaseObject
    {

        private int AngularVelocity;
        private Point Axix;
        private int Radius;

        public Satellite(int radius, int angularVelocity, Point axis, Size size) : base(new Point(0, 0), new Point(0,0), size)
        {
            Radius = radius;
            Axix = axis;
            AngularVelocity = angularVelocity;
            nameFile = GetNameFile("satellit");
            NumberFile = 0;
        }

        public override void Update()
        {
            Pos.X = Axix.X + (int)(Radius * Math.Cos(ToRadians(AngularVelocity)));
            Pos.Y = Axix.Y + (int)(Radius * Math.Sin(ToRadians(AngularVelocity)));

            AngularVelocity++;
        }


        private static double ToRadians(int degrees)
        {
            return degrees / 180.0 * Math.PI;
        }

    }
}
