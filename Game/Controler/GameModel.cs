using Game.Resources;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class GameModel
    {
        public int liveCount = 3;
        private int speed = 7;
        public int currentLevel = 1;

        public Hero Hero { get; set; }

        public GameModel()
        {
            Hero = new Hero(speed);
        }

        public void PickBox()
        {
            Hero.BoxPickUp(((Box)LoadParameters.box.DataBindings[0].DataSource).Weight);
        }

        public void NextLevel()
        {
            currentLevel++;
            Hero.boxCount = 0;
            Hero.enemyKillCount = 0;
            liveCount = 3;
        }

        public void HeroMove(Keys key, PictureBox mainHero)
        {
            switch (key)
            {
                case Keys.Left:
                    Hero.MoveLeft();
                    if (mainHero.Image != HeroMoveAnimations.left) mainHero.Image = HeroMoveAnimations.left;
                    break;
                case Keys.Right:
                    Hero.MoveRight();
                    if (mainHero.Image != HeroMoveAnimations.right) mainHero.Image = HeroMoveAnimations.right;
                    break;
                case Keys.Up:
                    Hero.MoveUp();
                    if (mainHero.Image != HeroMoveAnimations.up) mainHero.Image = HeroMoveAnimations.up;
                    break;
                case Keys.Down:
                    Hero.MoveDown();
                    if (mainHero.Image != HeroMoveAnimations.down) mainHero.Image = HeroMoveAnimations.down;
                    break;
                case Keys.Space:
                    if (LoadParameters.bullet.Left > 1280 || LoadParameters.bullet.Left < 0 || LoadParameters.bullet.Top < 0 || LoadParameters.bullet.Top > 720)
                    {
                        PlaySound.Play(Sounds.shoot);
                        LoadParameters.bullet.Location = new Point(mainHero.Location.X + mainHero.Height / 2, mainHero.Location.Y + mainHero.Width / 2);
                        Hero.bullet.IsExist = false;
                    }
                    break;
            }
        }

        public void Hit() => liveCount--;
    }
}
