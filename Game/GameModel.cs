using Game.Resources;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    class GameModel
    {
        /*public int sizeX { get; private set; } = 1280;
        public int sizeY { get; private set; } = 720;*/
        private int invulnerability = 0;
        private int count = 0;
        private int liveCount = 3;
        public Hero Hero { get; private set; }

        public GameModel(int Speed)
        {
            Hero = new Hero(Speed);
        }

        public void Interspect()
        {
            PictureBox deleteEnemy = null;
            foreach (PictureBox en in LoadParameters.enemyCount)
            {
                if (LoadParameters.bullet.Bounds.IntersectsWith(en.Bounds))
                {
                    PlaySound.Play(Sounds.hitEnemy);
                    Controls.Remove(en);
                    Controls.Remove(LoadParameters.bullet);
                    Hero.KillCountUpdate();
                    deleteEnemy = en;
                }
                if (MainHero.Bounds.IntersectsWith(en.Bounds))
                {
                    PlaySound.Play(Sounds.hitHeart);
                    if (count - invulnerability > 100)
                    {
                        liveCount--;
                        GetHP();
                        invulnerability = count;
                    }
                }
            }
            LoadParameters.enemyCount.Remove(deleteEnemy);
            if (MainHero.Bounds.IntersectsWith(LoadParameters.box.Bounds))
            {
                PlaySound.Play(Sounds.pickupBox);
                if (LoadParameters.box.Image.Equals(LoadBoxes.box1)) boxCount += 1;
                if (LoadParameters.box.Image.Equals(LoadBoxes.box3)) boxCount += 3;
                if (LoadParameters.box.Image.Equals(LoadBoxes.box5)) boxCount += 5;
                Controls.Remove(LoadParameters.box);
                LoadParameters.BoxSpecifications(MainHero);
                Controls.Add(LoadParameters.box);
            }
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
                    Hero.Shoot();
                    if (LoadParameters.bullet.Left > 1280 || LoadParameters.bullet.Left < 0 || LoadParameters.bullet.Top < 0 || LoadParameters.bullet.Top > 720)
                    {
                        PlaySound.Play(Sounds.shoot);
                        LoadParameters.bullet.Location = new Point(mainHero.Location.X + mainHero.Height / 2, mainHero.Location.Y + mainHero.Width / 2);
                        Hero.bullet.IsExist = false;
                        Controls.Add(LoadParameters.bullet);
                    }
                    break;
            }
        }
        private void GetHP()
        {
            LivePicture.SizeMode = PictureBoxSizeMode.Zoom;
            switch (liveCount)
            {
                case 3:
                    LivePicture.Image = HP._3hp;
                    break;
                case 2:
                    LivePicture.Image = HP._2hp;
                    break;
                case 1:
                    LivePicture.Image = HP._1hp;
                    break;
            }
        }

        private void GameWon()//пауза до нажатия enter
        {
            Keys isEnter = new Keys();
            PlaySound.Play(Sounds.win);
            EndOfGame.Visible = true;
            EndOfGame.Text = "Уровень пройден.";
            GameProgressTimer.Stop();
            currentLevel++;
            invulnerability = 0;
            LevelChange();
            liveCount = 3;
            GetHP();
            boxCount = 0;
            count = 0;
            foreach (PictureBox en in LoadParameters.enemyCount)
            {
                Controls.Remove(en);
            }
            LoadParameters.enemyCount.Clear();
            EndOfGame.Visible = false;
            LoadParameters.enemyCount.Clear();
            GameProgressTimer.Start();
        }

        private void GameOver()
        {
            PlaySound.Play(Sounds.lose);
            LivePicture.Image = HP._0hp;
            MainHero.Visible = false;
            EndOfGame.Visible = true;
            EndOfGame.Text = "Игра окончена.";
            GameProgressTimer.Stop();
        }
    }
}
