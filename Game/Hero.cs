using System;
using System.Drawing;

namespace Game
{
    class Hero
    {
        public Point location = new Point();
        private int speed;
        public event Action<Point> OnMove;

        public Hero(int Speed)
        {
            speed = Speed;
        }

        public void MoveLeft()
        { 
            if(location.X > 0) location.X -= speed;
            if(OnMove != null) OnMove(location);
        }

        public void MoveRight() 
        {
            if(location.X < 1190) location.X += speed;
            if (OnMove != null) OnMove(location);
        }

        public void MoveDown() 
        {
            if (location.Y < 590) location.Y += speed;
            if (OnMove != null) OnMove(location);
        }

        public void MoveUp() 
        {
            if(location.Y > 0) location.Y -= speed;
            if (OnMove != null) OnMove(location);
        }
    }
}
