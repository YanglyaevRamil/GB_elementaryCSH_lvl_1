using System;
using System.Drawing;
using Asteroids.Properties;
using System.Windows.Forms;
using System.IO;

namespace Asteroids.Scenes
{
    class Game : BaseScene
    {
        private static Random random = new Random();
        static Timer _timer;
        static int score;

        static Planet _planet;
        static Asteroid[] _asteroids;
        static Star[] _stars;
        static Bullet _bullet;
        static Ship _ship;
        static Ufo _ufo;
        static Satellite _satellite;
        static Explosion _explosion;
        static HealerBox _healerBox;
        static BonusBox _bonusBox;

        private static int bonusScore, killAsteroid, bonusBorder;

        public override void Init(Form form)
        {
            try
            {
                base.Init(form);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _timer.Stop();
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Load();

            _timer = new Timer { Interval = 20 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            _ship.DieEvent += _ship_DieEvent;

        }
        public override void SceneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && _bullet == null)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 25), new Point(15, 0), new Size(54, 9));
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                _ship.Up();
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                _ship.Down();
            }
            if (e.KeyCode == Keys.Back)
            {
                SceneManager
                    .Get()
                    .Init<MenuScene>(_form)
                    .Draw();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        public override void Draw()
        {
            // Фон
            Buffer.Graphics.DrawImage(Resources.background, new Rectangle(0, 0, Width, Height));

            // Звезды
            foreach (var star in _stars)
            {
                star.Draw();
            }

            // Планета и Спутник
            _planet.Draw();
            _satellite.Draw();

            // Астеройды
            foreach (var asteroid in _asteroids)
            {
                if (asteroid != null) asteroid.Draw();
            }
            // Лазер
            if (_bullet != null) _bullet.Draw();

            // НЛО
            _ufo.Draw();

            // Box
            if (_healerBox != null) _healerBox.Draw();
            if (_bonusBox != null) _bonusBox.Draw();

            // Взрыв
            if (_explosion != null) _explosion.Draw();

            // Корабль 
            if (_ship != null)
            {
                _ship.Draw();
                Buffer.Graphics.DrawString($"ENERGY: {_ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 10, 10);
            }

            // Счет
            Buffer.Graphics.DrawString($"SCORE: {score}", SystemFonts.DefaultFont, Brushes.White, Width - 100, Height - 50);
           
            Buffer.Render();
        }
        public void Load()
        {
            try
            {
                killAsteroid = 0;
                bonusScore = 0;
                score = 0;
                bonusBorder = 5;

                _planet = new Planet(new Point(400, 150), new Point(0, 0), new Size(200, 200));
                _satellite = new Satellite(150, 10, new Point(_planet.Rect.X + _planet.Rect.Width / 2 - 25, _planet.Rect.Y + _planet.Rect.Height / 2 - 25), new Size(50, 50));

                _asteroids = new Asteroid[10];

                for (int i = 0; i < _asteroids.Length; i++)
                {
                    int size = random.Next(20, 30);
                    _asteroids[i] = new Asteroid(new Point(Width - 150, i * 20 + 20), new Point(-i - 2, -i - 2), new Size(size, size));
                    _asteroids[i].DieEvent += _asteroid_DieEvent; ;
                }
                _stars = new Star[10];
                for (int i = 0; i < _stars.Length; i++)
                {
                    _stars[i] = new Star(new Point(Width - 150, i * 40 + 15), new Point(i + 1, i + 1), new Size(10, 10));
                }

                _ship = new Ship(new Point(10, 200), new Point(0, 15), new Size(45, 60));
                _ship.DieEvent += _ship_DieEvent;

                _ufo = new Ufo(new Point(0, 0), new Point(5, 0), new Size(76, 31));

                GameLog.LogGame.Add(new Message("Начало игры", _ship.Energy, score));
            }
            catch (GameObjectException e)
            {
                _timer.Stop();
                MessageBox.Show($"{e.Message}. Переменная {e.Name}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        // *******************************************************************
        // События 

        // Смерть бонусного элемента
        private static void _bonusBox_DieEvent(object sender, EventArgs e)
        {
            bonusScore += _bonusBox.Bonus;
            GameLog.SetLogGame("Получен бонус", _ship.Energy, score);
        }
        // Смерть аптечки
        private static void _healerBox_DieEvent(object sender, EventArgs e)
        {
            _ship.EnergyLow(_healerBox.Heal);
            GameLog.SetLogGame("Получена аптечка", _ship.Energy, score);
        }
        // Смерть астеройда
        private static void _asteroid_DieEvent(object sender, EventArgs e)
        {
            Asteroid a = (Asteroid)sender;
            _explosion = new Explosion(new Point(a.PositionCustom.X, a.PositionCustom.Y), new Point(0, 0), new Size(80, 60), 80);
            killAsteroid++;
            GameLog.SetLogGame("Убит астероид", _ship.Energy, score);
        }
        // Смерть корабля
        private static void _ship_DieEvent(object sender, EventArgs e)
        {
            _timer.Stop();
            Ship s = (Ship)sender;
            Buffer.Graphics.DrawString($"Game Over \nDamage: {s.LastDamage} \n<Backspace> - в меню", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            GameLog.SetLogGame("Корабль уничтожен", _ship.Energy, score);
            GameLog.WriteLogFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Resources\\FileLog\\GameLog.txt");
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

        public void Update()
        {
            score = killAsteroid + bonusScore;

            foreach (var star in _stars)
            {
                star.Update();
            }

            if (_bullet != null)
            {
                _bullet.Update();

                if (_bullet.Rect.X >= Width)
                {
                    _bullet = null;
                }
                else
                {
                    if (_bonusBox != null)
                    {
                        if (_bonusBox.Collision(_bullet))
                        {
                            _bullet = null;

                            _bonusBox = (BonusBox)_bonusBox.Die(); ;
                        }
                    }
                    if (_bullet != null)
                    {
                        if (_healerBox != null)
                        {
                            if (_healerBox.Collision(_bullet))
                            {
                                _bullet = null;

                                _healerBox = (HealerBox)_healerBox.Die();
                            }
                        }
                    }
                }
            }

            _ufo.Update();

            if (_healerBox != null)
            {
                _healerBox.Update();
                if (_healerBox.Rect.Y >= Height) _healerBox = null;
            }
            else
            {
                if (_ufo.HealBoxReady)
                {
                    _healerBox = _ufo.GetHealerBox(_ufo.PositionCustom.X, _ufo.PositionCustom.Y);
                    _healerBox.DieEvent += _healerBox_DieEvent;
                }
            }

            if (_bonusBox != null)
            {
                _bonusBox.Update();
                if (_bonusBox.Rect.Y >= Height) _bonusBox = null;
            }
            else
            {
                if (killAsteroid >= bonusBorder)
                {
                    _bonusBox = _ufo.GetBonusBox(_ufo.PositionCustom.X, _ufo.PositionCustom.Y);
                    _bonusBox.DieEvent += _bonusBox_DieEvent;
                    bonusBorder += 5;
                }
            }

            _satellite.Update();

            for (int i = 0; i < _asteroids.Length; i++)
            {
                if (_explosion != null)
                {
                    _explosion.Update();
                    if (_explosion.Lifetime == 0)
                    {
                        _explosion = null;
                    }
                }

                if (_asteroids[i] == null)
                {
                    continue;
                }

                _asteroids[i].Update();

                if (_bullet != null && _asteroids[i].Collision(_bullet))
                {
                    _bullet = null;

                    _asteroids[i] = (Asteroid)_asteroids[i].Die(); ;

                    continue;
                }

                if (_asteroids[i] != null && _asteroids[i].Collision(_ufo))
                {
                    _explosion = new Explosion(new Point(_asteroids[i].PositionCustom.X, _asteroids[i].PositionCustom.Y), new Point(0, 0), new Size(80, 60), 80);
                    _asteroids[i].PositionCustom = GetPointBorder(_asteroids[i]);
                }

                if (_ship != null && _asteroids[i].Collision(_ship))
                {
                    _ship.EnergyLow(_asteroids[i].Damage);
                    if (_ship.Energy <= 0)
                    {
                        _ship.Die();
                        break;
                    }
                    _asteroids[i] = (Asteroid)_asteroids[i].Die(); ;
                    continue;
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _timer.Stop();
        }
    }
}
