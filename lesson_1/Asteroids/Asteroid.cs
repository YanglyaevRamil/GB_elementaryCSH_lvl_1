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
    class Asteroid
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        static Random random = new Random();
        protected int rnd;

        protected string[] nameFile; 

        public Asteroid(Point pos, Point dir, Size size)
        {
            nameFile = GetNameFile("meteorBrown");
            
            rnd = random.Next(0, nameFile.Length-1);

            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        // *******************************************************************
        // возвращает массив полных имен данных в каталоге Resources по параметру 
        protected static string[] GetNameFile(string nameFile)
        {
            return Directory.GetFiles(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + "\\Resources", $"{nameFile}*");
        }

        // *******************************************************************
        // возвращает карту пикселей однгого из случайных изображений 
        protected Bitmap LoadRndImg()
        {
            return new Bitmap(nameFile[rnd]);
        }

        // *******************************************************************
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage((Image)LoadRndImg(), new Rectangle(pos.X, pos.Y, size.Width, size.Height));
        }

        public virtual void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X + size.Width > Game.Width) dir.X = -dir.X;

            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y + size.Height > Game.Height) dir.Y = -dir.Y;
        }
    }
}
