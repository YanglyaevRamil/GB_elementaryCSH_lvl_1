using Asteroids.Scenes;
using System;
using System.Drawing;
using System.IO;

namespace Asteroids
{
    abstract class BaseObject : ICollision
    {
        public event EventHandler DieEvent;

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public Point PositionCustom
        {
            set 
            {
                if (Pos.X >= (-Size.Width + 0) && Pos.Y >= (-Size.Height + 0) && Pos.X <= (Game.Width + Size.Width) && Pos.Y <= (Game.Height + Size.Height))
                {
                    Pos.X = value.X;
                    Pos.Y = value.Y;
                }
                else
                {
                    throw new GameObjectException("Объект за границей видимого экрана", nameof(Pos));
                }
            }
            get { return new Point(Pos.X, Pos.Y); }
        }
        public Point DirCustom
        {

            set 
            {
                if (Dir.X <= 10 && Dir.Y <= 10)
                {
                    Dir.X = value.X;
                    Dir.Y = value.Y;
                }
                else
                {
                    throw new GameObjectException("Слишком большая скорость для объекта", nameof(Dir));
                }

            }
            get { return new Point(Dir.X, Dir.Y); }
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(Pos, Size);
            }
        }

        protected static Random random = new Random();
        protected int NumberFile = 0;

        protected string[] nameFile;

        public BaseObject()
        {

        }
        public BaseObject(Point Pos, Point Dir, Size Size)
        {
            if (Pos.X >= (-Size.Width + 0) && Pos.Y >= (-Size.Height + 0) && Pos.X <= (Game.Width + Size.Width) && Pos.Y <= (Game.Height + Size.Height))
            {
                this.Pos = Pos;
            }
            else
            {
                throw new GameObjectException("Объект за границей видимого экрана", nameof(Pos));
            }

            if (Dir.X <= 50 && Dir.Y <= 50)
            {
                this.Dir = Dir;
            }
            else
            {
                throw new GameObjectException("Большая скорость для объекта", nameof(Dir));
            }

            if (Size.Width >= 0 && Size.Height >= 0)
            {
                if (Size.Width <= 200 && Size.Height <= 200)
                {
                    this.Size = Size;
                }
                else
                {
                    throw new GameObjectException("Большой размер объекта", nameof(Size));
                }
            }
            else
            {
                throw new GameObjectException("Отрицательный размер объекта", nameof(Size));
            }
        }

        // *******************************************************************
        // Обработка сталкивания
        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }

        // *******************************************************************
        // возвращает массив полных имен данных в каталоге Resources по параметру 
        protected static string[] GetNameFile(string nameFile)
        {
            return Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Resources", $"{nameFile}*");
        }

        // *******************************************************************
        // возвращает карту пикселей однгого из изображений 
        protected Bitmap LoadImg()
        {
            return new Bitmap(nameFile[NumberFile]);
        }

        // *******************************************************************
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage((Image)LoadImg(), new Rectangle(Pos.X, Pos.Y, Size.Width, Size.Height));
        }

        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;

            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X + Size.Width > Game.Width) Dir.X = -Dir.X;

            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y + Size.Height > Game.Height) Dir.Y = -Dir.Y;
        }
        // *******************************************************************
        // Смерть объекта 
        public virtual object Die()
        {
            if (DieEvent != null)
            {
                DieEvent.Invoke(this, new EventArgs());
            }
            return null;
        }

    }
}
