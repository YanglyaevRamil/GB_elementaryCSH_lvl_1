using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        private static Random random = new Random();
        static Timer timer = new Timer();

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        static Asteroid[] _asteroids;
        static Star[] _stars;
        static Bullet _bullet;
        static Ufo _ufo;
        static Satellite _satellite;
        static Explosion _explosion;

        private static int width;
        private static int height;

        public static int Width
        {
            get { return width; }
            set
            {
                if (value <= 1000)
                {
                    if (value >= 200)
                    {
                        width = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Width", "Error - занчение Width не может быть меньше 200, измените параметр!");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Width", "Error - занчение Width не может быть болше 1000, измените параметр!");
                }
            }
        }
        public static int Height
        {
            get { return height; }
            set
            {
                if (value <= 1000)
                {
                    if (value >= 200)
                    {
                        height = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Error - занчение Height не может быть меньше 200, измените параметр!");
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Error - занчение Height не может быть болше 1000, измените параметр!");
                }
            }
        }

        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            timer.Interval = 40;
            timer.Tick += Timer_Tick;
            timer.Start();

            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

                Load();
            }
            catch (ArgumentOutOfRangeException e)
            {
                timer.Stop();
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Draw()
        {
            // Фон
            Buffer.Graphics.DrawImage(Resources.background, new Rectangle(0, 0, Width, Height));

            // Звезды
            foreach (var star in _stars)
            {
                star.Draw();
            }

            // Планета
            Buffer.Graphics.DrawImage(Resources.planet, new Rectangle(100, 100, 200, 200));

            // Астеройды
            foreach(var asteroid in _asteroids)
            {
                asteroid.Draw();
            }

            // Лазер
            _bullet.Draw();

            // НЛО
            _ufo.Draw();

            // Спутник
            _satellite.Draw();

            _explosion.Draw();

            Buffer.Render();
        }

        public static void Update()
        {
            foreach (var asteroid in _asteroids)
            {
                //====================================Task_3=======================================================
                // Сделать так, чтобы при столкновениях пули с астероидом они регенерировались в разных концах экрана.
                asteroid.Update();
                if (asteroid.Collision(_bullet))
                {
                    asteroid.PositionCustom = GetPointBorder(asteroid);
                }
                //=================================================================================================

                if (asteroid.Collision(_ufo))
                {
                    _explosion = new Explosion(new Point(asteroid.PositionCustom.X, asteroid.PositionCustom.Y), new Point(0, 0), new Size(80, 60));
                    
                    asteroid.PositionCustom = GetPointBorder(asteroid);
                }
            }
            
            foreach (var star in _stars)
            {
                star.Update();
            }

            _bullet.Update();

            _ufo.Update();

            _satellite.Update();

        }

        public static void Load()
        {
            try
            {
                _asteroids = new Asteroid[10];

                for (int i = 0; i < _asteroids.Length; i++)
                {
                    var size = random.Next(20, 30);
                    _asteroids[i] = new Asteroid(new Point(width - 150, i * 20 + 20), new Point(-i - 2, -i - 2), new Size(size, size));
                }

                _stars = new Star[10];
                for (int i = 0; i < _stars.Length; i++)
                {
                    _stars[i] = new Star(new Point(width - 150, i * 40 + 15), new Point(i + 1, i + 1), new Size(10, 10));
                }

                _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(54, 9));

                _ufo = new Ufo(new Point(0, 0), new Point(5, 0), new Size(80, 60));

                _satellite = new Satellite(130, 10, new Point(140, 180), new Size(75, 55));

                _explosion = new Explosion();
            }
            catch (GameObjectException e)
            {
                timer.Stop();
                MessageBox.Show($"{e.Message}. Переменная {e.Name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

        }

        // *******************************************************************
        // Возвращает случайную точку на границе области формы
        private static Point GetPointBorder(Asteroid asteroid)
        {
            int SideWorld = random.Next(0, 3);
            switch (SideWorld)
            {
                case 0:
                    return new Point(0, random.Next(0, Height - asteroid.Rect.Height));
                case 1:
                    return new Point(random.Next(0, Width - asteroid.Rect.Width), 0);
                case 2:
                    return new Point(Width - asteroid.Rect.Width, random.Next(0, Height - asteroid.Rect.Height));
                default:
                    return new Point(random.Next(0, Width - asteroid.Rect.Width), Height - asteroid.Rect.Height);
            }
        }

    }
}
