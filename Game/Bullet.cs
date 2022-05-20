using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    class Bullet
    {
        private const int bulletSpeed = 14;

        public void Move(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    LoadParameters.bullet.Left -= bulletSpeed;
                    break;
                case Keys.Right:
                    LoadParameters.bullet.Left += bulletSpeed;
                    break;
                case Keys.Down:
                    LoadParameters.bullet.Top += bulletSpeed;
                    break;
                case Keys.Up:
                    LoadParameters.bullet.Top -= bulletSpeed;
                    break;
            }
        }
    }
}
