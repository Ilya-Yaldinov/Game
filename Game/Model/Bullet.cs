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
        public bool IsExist { get; set; }
        public Side Side { get; set; }

        public void Move(Side side)
        {
            if (!IsExist)
            {
                IsExist = true;
                Side = side;
            }
            switch (Side)
            {
                case Side.Left:
                    LoadParameters.bullet.Left -= bulletSpeed;
                    break;
                case Side.Right:
                    LoadParameters.bullet.Left += bulletSpeed;
                    break;
                case Side.Down:
                    LoadParameters.bullet.Top += bulletSpeed;
                    break;
                case Side.Up:
                    LoadParameters.bullet.Top -= bulletSpeed;
                    break;
            }
        }
    }
}
