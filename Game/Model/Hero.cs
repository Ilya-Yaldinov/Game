using System;
using System.Drawing;

namespace Game
{
    class Hero : Character
    {
        public int enemyKillCount { get; private set; }
        public int boxCount { get; private set; }

        public Hero(int speed) : base(speed){ }

        public void KillCountUpdate()
        {
            enemyKillCount++;
        }

        public void BoxCountUpdate()
        {
            boxCount++;
        }

        public void BoxPickUp()
        {
            
        }
    }
}
