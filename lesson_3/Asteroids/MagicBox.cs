using Asteroids.Scenes;
using System.Drawing;


namespace Asteroids
{
    class MagicBox : BaseObject
    {
        protected static int rndPos = 200;
        public static int RndPos { get { return rndPos; } }

        public MagicBox(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
    }

    class HealerBox : MagicBox
    {

        private int damage = -30; 

        public int Heal { get { return damage; } }
        public HealerBox(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("box");

            NumberFile = 2;

            rndPos = random.Next(100, Game.Width - 100);
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
        }
    }

    class BonusBox : MagicBox
    {
        private int bonus = 3;
        public int Bonus { get { return bonus; } }


        public BonusBox(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            nameFile = GetNameFile("box");

            NumberFile = 3;

            rndPos = random.Next(100, Game.Width - 100);
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
        }
    }

}
