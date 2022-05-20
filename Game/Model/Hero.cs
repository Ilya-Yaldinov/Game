using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Hero : Character
    {
        public Bullet bullet { get; }
        public int enemyKillCount { get; private set; }
        public int boxCount { get; private set; }

        public Hero(int speed) : base(speed){ bullet = new Bullet(); }

        public void KillCountUpdate()
        {
            enemyKillCount++;
        }

        public void BoxCountUpdate()
        {
            boxCount++;
        }

        public void Shoot()
        {
            bullet.Move(Side);
        }

        public void BoxPickUp()
        {
            
        }
    }
}
