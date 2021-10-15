using Asteroids.Scenes;
using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Form form = new Form()
            {
                MinimumSize = new System.Drawing.Size(800, 500),
                MaximumSize = new System.Drawing.Size(800, 500),
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Asteroids"
            };
            form.Show();

            SceneManager
                .Get()
                .Init<MenuScene>(form)
                .Draw();

            Application.Run(form);
        }
    }
}
