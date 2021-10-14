using Asteroids.Scenes;
using System.Drawing;

namespace Asteroids
{
    class Ufo : BaseObject
    {
        private int distanceTraveled;

        private bool healBoxReady;

        public bool HealBoxReady { get { return healBoxReady; } }

        public Ufo(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("ufo");

            NumberFile = 0;

            distanceTraveled = 0;

            healBoxReady = false;
        }

        public override void Update()
        {
            Pos.X += Dir.X;

            if (Pos.X >= Game.Width)
            {
                Pos.X = 0;
                distanceTraveled++;

            }

            if (distanceTraveled == 1)
            {
                if (Pos.X >= HealerBox.RndPos )
                {
                    healBoxReady = true;
                    distanceTraveled = 0;
                }
            }
        }

        //delegate T GetBox<T>(int x, int y);

        public BonusBox GetBonusBox(int x, int y)
        {
            return new BonusBox(new Point(x, y), new Point(0, 5), new Size(40, 50));
        }
        public HealerBox GetHealerBox(int x, int y)
        {
            healBoxReady = false;
            return new HealerBox(new Point(x, y), new Point(0, 5), new Size(40, 40));
        }
    }
}
