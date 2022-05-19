using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class GameBackGround : Form
    {
        private GameModel gameModel;
        private EventHandler action;
        private PictureBox bullet = null;
        private PictureBox box = null;
        private PictureBox enemySprite = null;
        private int bulletSpeed = 0;
        private int gameSpeed = 0;
        private int count = 0;
        private int invulnerability = 0;
        private int enemyKillCount = 0;
        private int boxCount = 0;
        private int liveCount = 3;
        private int winCount = 50;
        private Point direction = Point.Empty;
        private Keys lastKey = Keys.None;
        private Keys bulletKey = Keys.Right;
        private List<PictureBox> enemyCount = new List<PictureBox>();
        private Dictionary<string, Image> heroAnimation = new Dictionary<string, Image>();
        private Dictionary<string, Image> boxes = new Dictionary<string, Image>();

        public GameBackGround()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void LoadAnimations()
        {
            heroAnimation.Add("left", Resources.HeroMovement.hero_left);
            heroAnimation.Add("up", Resources.HeroMovement.hero_up);
            heroAnimation.Add("right", Resources.HeroMovement.hero_right);
            heroAnimation.Add("down", Resources.HeroMovement.hero_down);
        }

        private void LoadBoxes()
        {
            boxes.Add("1box", Resources.BoxPicture._1box);
            boxes.Add("3box", Resources.BoxPicture._3box);
            boxes.Add("5box", Resources.BoxPicture._5box);
        }

        private void BoxSpecifications()
        {
            Random random = new Random();
            box = new PictureBox();
            box.Image = GetBoxImage();
            box.Size = new Size(MainHero.Height/2, MainHero.Width/2);
            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.BackColor = Color.Transparent;
            box.Location = new Point(random.Next(10, 1000), random.Next(10, 600));

            this.Controls.Add(box);
        }

        private Image GetBoxImage()
        {
            List<Image> images = new List<Image>();
            Random random = new Random();
            for(int i = 0; i < 6; i++)
            {
                images.Add(boxes["1box"]);
                if(i<3) images.Add(boxes["3box"]);
            }
            images.Add(boxes["5box"]);

            return images.ElementAt(random.Next(0,10));
        }

        private void BulletSpecifications()
        {
            bullet = new PictureBox();
            bullet.BorderStyle = BorderStyle.None;
            bullet.Size = new Size(10, 10);
            bullet.BackColor = Color.Red;
        }

        private void EnemySpawn()
        {
            enemySprite = new PictureBox();
            enemySprite.Image = Resources.EnemyMovement.ghost;
            enemySprite.Size = new Size(MainHero.Height, MainHero.Width);
            enemySprite.SizeMode = PictureBoxSizeMode.Zoom;
            enemySprite.BackColor = Color.Transparent;
            enemySprite.Location = new Point(640, 360);
            enemyCount.Add(enemySprite);

            this.Controls.Add(enemySprite);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            LoadAnimations();
            LoadBoxes();
            BulletSpecifications();
            BoxSpecifications();
            MainHero.Image = heroAnimation["right"];
            gameSpeed = 7;
            bulletSpeed = gameSpeed * 2;
            gameModel = new GameModel(gameSpeed);
            gameModel.hero.location = MainHero.Location;
            gameModel.hero.OnMove += location =>
            {
                MainHero.Location = location;
            };
        }

        private void HeroMove(Keys key)
        {
            if(key != Keys.Space) lastKey = key;
            switch (key)
            {
                case Keys.Left:
                    gameModel.hero.MoveLeft();
                    if (MainHero.Image != heroAnimation["left"]) MainHero.Image = heroAnimation["left"];
                    break;
                case Keys.Right:
                    gameModel.hero.MoveRight();
                    if (MainHero.Image != heroAnimation["right"]) MainHero.Image = heroAnimation["right"];
                    break;
                case Keys.Up:
                    gameModel.hero.MoveUp();
                    if (MainHero.Image != heroAnimation["up"]) MainHero.Image = heroAnimation["up"];
                    break;
                case Keys.Down:
                    gameModel.hero.MoveDown();
                    if (MainHero.Image != heroAnimation["down"]) MainHero.Image = heroAnimation["down"];
                    break;
                case Keys.Space:
                    if (bullet.Left > 1280 || bullet.Left < 0 || bullet.Top < 0 || bullet.Top > 720)
                    {
                        bulletKey = lastKey;
                        bullet.Location = new Point(MainHero.Location.X + MainHero.Height/2, MainHero.Location.Y + MainHero.Width/2);
                        this.Controls.Add(bullet);
                    }
                    break;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            MoveTimer.Start();
            MoveTimer.Tick -= action;
            action = (obj, args) => HeroMove(e.KeyCode);
            MoveTimer.Tick += action;
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            MoveTimer.Stop();
        }

        private void Interspect()//смерть сделать
        {
            PictureBox deletEnemy = null;
            foreach(PictureBox en in enemyCount)
            {
                if (bullet.Bounds.IntersectsWith(en.Bounds))
                {
                    this.Controls.Remove(en);
                    this.Controls.Remove(bullet);
                    enemyKillCount++;
                    deletEnemy = en;
                }
                if (MainHero.Bounds.IntersectsWith(en.Bounds))
                {
                    if(count - invulnerability > 100)
                    {
                        liveCount--;
                        invulnerability = count;
                    }
                }
            }
            enemyCount.Remove(deletEnemy);
            if (MainHero.Bounds.IntersectsWith(box.Bounds))
            {
                if (box.Image.Equals(boxes["1box"])) boxCount += 1;
                if (box.Image.Equals(boxes["3box"])) boxCount += 3;
                if (box.Image.Equals(boxes["5box"])) boxCount += 5;
                this.Controls.Remove(box);
                BoxSpecifications();
            }
        }

        private void BulletMovement(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    bullet.Left -= bulletSpeed;
                    break;
                case Keys.Right:
                    bullet.Left += bulletSpeed;
                    break;
                case Keys.Down:
                    bullet.Top += bulletSpeed;
                    break;
                case Keys.Up:
                    bullet.Top -= bulletSpeed;
                    break;
            }
        }

        private void RandomMoveEnemys()
        {
            Random random = new Random();
            direction.X = random.Next(-1, 2);
            direction.Y = random.Next(-1, 2);
            MoveEnemys();
        }

        private void MoveEnemys()//за рамки выходит
        {
            foreach (PictureBox en in enemyCount)
            {
                Point p = en.Location;
                en.Left += direction.X * gameSpeed * 4;
                en.Top += direction.Y * gameSpeed * 4;
                if (p.X < 0 || p.X > 400) direction.X *= -1;
                if (p.Y < 0 || p.Y > 1000) direction.Y *= -1;
            }
        }

        private void GameProgressTimer_Tick(object sender, EventArgs e)
        {
            GetHP();
            Interspect();
            KillLable.Text = $"Противников убито: {enemyKillCount}";
            BoxCount.Text = $"Коробочек собранно: {boxCount}";
            count++;
            Console.WriteLine(count);
            BulletMovement(bulletKey);
            if(count % 10 == 0) RandomMoveEnemys();
            if (count % 400 == 0) EnemySpawn();
            if (liveCount == 0) GameOver();
            if (boxCount >= winCount) GameWon();
        }

        private void GameWon()
        {
            EndOfGame.Visible = true;
            EndOfGame.Text = "Вы победили.";
            GameProgressTimer.Stop();
        }

        private void GetHP()
        {
            LivePicture.SizeMode = PictureBoxSizeMode.Zoom;
            switch (liveCount)
            {
                case 3:
                    LivePicture.Image = Resources.HP._3hp;
                    break;
                case 2:
                    LivePicture.Image = Resources.HP._2hp;
                    break;
                case 1:
                    LivePicture.Image = Resources.HP._1hp;
                    break;
            }
        }

        private void GameOver()
        {
            LivePicture.Image = Resources.HP._0hp;
            MainHero.Visible = false;
            EndOfGame.Visible = true;
            EndOfGame.Text = "Игра окончена.";
            GameProgressTimer.Stop();
        }
    }
}
