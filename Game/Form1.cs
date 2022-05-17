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
        private int gameSpeed;
        private GameModel gameModel;
        private EventHandler action;
        private PictureBox bullet = null;
        private int bulletSpeed;
        private int count = 0;
        private int enemyKillCount = 0;
        private int liveCount = 3;
        private int boxCount = 0;
        private Point direction = Point.Empty;
        private Keys keyBeforeDown = Keys.None;
        private Keys lastKey = Keys.None;
        private Keys bulletKey = Keys.Right;
        private PictureBox enemySprite;
        private List<PictureBox> enemyCount = new List<PictureBox>();
        private Dictionary<string, Image> heroAnimation = new Dictionary<string, Image>();
        private Dictionary<string, Image> enemy = new Dictionary<string, Image>();
        private Dictionary<string, Image> backgrounds = new Dictionary<string, Image>();

        public GameBackGround()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void LoadEnemy()
        {
            enemy.Add("ghost_l", EnemyMovement.ghost_l);
        }

        private void LoadBackGround()
        {
            backgrounds.Add("lvl_1", GameBG.lvl_1);
        }

        private void LoadAnimations()
        {
            heroAnimation.Add("l_d", HeroMovement.l_d);
            heroAnimation.Add("r_u", HeroMovement.r_u);
            heroAnimation.Add("r_d", HeroMovement.r_d);
        }

        private void BulletSpecifications()
        {
            bullet = new PictureBox();
            bullet.BorderStyle = BorderStyle.None;
            bullet.Size = new Size(10, 10);
            bullet.BackColor = Color.White;
        }

        private void EnemySpawn()
        {
            enemySprite = new PictureBox();
            enemySprite.Image = enemy["ghost_l"];
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
            LoadBackGround();
            LoadEnemy();
            BulletSpecifications();
            EnemySpawn();
            MainHero.Image = heroAnimation["r_d"];
            gameSpeed = 7;
            bulletSpeed = gameSpeed * 2;
            gameModel = new GameModel(gameSpeed);
            gameModel.hero.location = MainHero.Location;
            gameModel.hero.OnMove += location =>
            {
                MainHero.Location = location;
            };
        }

        private void MoveTimer_Tick(Keys key)
        {
            if(key != Keys.Space) lastKey = key;
            switch (key)
            {
                case Keys.Left:
                    keyBeforeDown = key;
                    gameModel.hero.MoveLeft();
                    if (MainHero.Image != heroAnimation["l_d"]) MainHero.Image = heroAnimation["l_d"];
                    break;
                case Keys.Right:
                    keyBeforeDown = key;
                    gameModel.hero.MoveRight();
                    if (MainHero.Image != heroAnimation["r_d"]) MainHero.Image = heroAnimation["r_d"];
                    break;
                case Keys.Up:
                    gameModel.hero.MoveUp();
                    if (MainHero.Image != heroAnimation["r_u"]) MainHero.Image = heroAnimation["r_u"];
                    break;
                case Keys.Down:
                    gameModel.hero.MoveDown();
                    if (keyBeforeDown == Keys.Left)
                    {
                        if (MainHero.Image != heroAnimation["l_d"]) MainHero.Image = heroAnimation["l_d"];
                    }
                    else
                    {
                        if (MainHero.Image != heroAnimation["r_d"]) MainHero.Image = heroAnimation["r_d"];
                    }
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
            action = (obj, args) => MoveTimer_Tick(e.KeyCode);
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
                if (bullet.Bounds.IntersectsWith(en.Bounds) || en.Bounds.Contains(bullet.Bounds) || en.Bounds.IntersectsWith(bullet.Bounds))
                {
                    this.Controls.Remove(en);
                    this.Controls.Remove(bullet);
                    enemyKillCount++;
                    deletEnemy = en;
                }
                if (MainHero.Bounds.IntersectsWith(en.Bounds) || en.Bounds.IntersectsWith(MainHero.Bounds))
                {
                    MainHero.Visible = false;
                }
            }
            enemyCount.Remove(deletEnemy);
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
            Interspect();
            KillLable.Text = $"Противников убито: {enemyKillCount}";
            BoxCount.Text = $"Коробочек собранно: {boxCount}";
            count++;
            Console.WriteLine(count);
            BulletMovement(bulletKey);
            if(count % 10 == 0) RandomMoveEnemys();
            if (count % 400 == 0) EnemySpawn();
        }
    }
}
