using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class Enemy : Character
    {
        private Point direction = Point.Empty;
        private int enemyMoveSpeed { get; set; }

        public Enemy(int speed) : base(speed) 
        {
            enemyMoveSpeed = speed;
        }

        public void Move()
        {
            Random random = new Random();
            direction.X = random.Next(-1, 2);
            direction.Y = random.Next(-1, 2);
            ChangePosition();
        }

        private void ChangePosition()//за рамки выходит
        {
            foreach (PictureBox enemy in LoadParameters.enemyCount)
            {
                Point p = enemy.Location;
                enemy.Left += direction.X * enemyMoveSpeed * 3;
                enemy.Top += direction.Y * enemyMoveSpeed * 3;
                if (p.X < 0 || p.X > 400) direction.X *= -1;
                if (p.Y < 0 || p.Y > 1000) direction.Y *= -1;
            }
        }
    }
}
