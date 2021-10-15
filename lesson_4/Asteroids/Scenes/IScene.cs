using System.Windows.Forms;

namespace Asteroids.Scenes
{
    interface IScene
    {
        void Init(Form form);
        void Draw();
    }
}
