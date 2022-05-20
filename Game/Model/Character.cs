using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Character
    {
        protected Side Side { get; set; }
        public Point location = new Point();
        public int speed;
        public event Action<Point> OnMove;

        public Character(int speed)
        {
            this.speed = speed;
        }

        public void MoveLeft()
        {
            Side = Side.Left;
            if (location.X > 0) location.X -= speed;
            if (OnMove != null) OnMove(location);
        }

        public void MoveRight()
        {
            Side = Side.Right;
            if (location.X < 1190) location.X += speed;
            if (OnMove != null) OnMove(location);
        }

        public void MoveDown()
        {
            Side = Side.Down;
            if (location.Y < 590) location.Y += speed;
            if (OnMove != null) OnMove(location);
        }

        public void MoveUp()
        {
            Side = Side.Up;
            if (location.Y > 100) location.Y -= speed;
            if (OnMove != null) OnMove(location);
        }
    }
}
