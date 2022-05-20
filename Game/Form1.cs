using Game.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public partial class GameBackGround : Form
    {
        private GameModel gameModel;
        private EventHandler action;
        private int bulletSpeed = 0;
        private int gameSpeed = 0;
        private int count = 0;
        private int invulnerability = 0;
        private int enemyKillCount = 0;
        private int boxCount = 0;
        private int liveCount = 3;
        private int winCount = 25;
        private int currentLevel = 1;
        private Point direction = Point.Empty;
        private Keys lastKey = Keys.None;
        private Keys bulletKey = Keys.Right;
        LoadParameters loadParameters = new LoadParameters();
        
        

        public GameBackGround()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            loadParameters.Start(MainHero);
            this.Controls.Add(loadParameters.box);
            LevelChange();
            GetHP();
            MainHero.Image = loadParameters.heroAnimation["right"];
            gameSpeed = 7;
            bulletSpeed = gameSpeed * 2;
            gameModel = new GameModel(gameSpeed);
            gameModel.hero.location = MainHero.Location;
            gameModel.hero.OnMove += location =>
            {
                MainHero.Location = location;
            };
        }

        private void LevelChange()
        {
            switch (currentLevel)
            {
                case 1:
                    this.BackgroundImage = GameBG.lvl_1;
                    loadParameters.enemyImage = EnemyMovement.spider;
                    break;
                case 2:
                    this.BackgroundImage = GameBG.lvl_2;
                    loadParameters.enemyImage = EnemyMovement.skelet;
                    break;
                case 3:
                    this.BackgroundImage = GameBG.lvl_3;
                    loadParameters.enemyImage = EnemyMovement.ghost;
                    break;
                case 4:
                    this.BackgroundImage = GameBG.lvl_4;
                    loadParameters.enemyImage = EnemyMovement.zombi;
                    break;
                case 5:
                    this.BackgroundImage = GameBG.lvl_5;
                    loadParameters.enemyImage = EnemyMovement.eye;
                    break;
                default:
                    EndOfGame.Visible = true;
                    EndOfGame.Text = "Вы победили";
                    GameProgressTimer.Stop();
                    break;

            }
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void HeroMove(Keys key)
        {
            if(key != Keys.Space) lastKey = key;
            switch (key)
            {
                case Keys.Left:
                    gameModel.hero.MoveLeft();
                    if (MainHero.Image != loadParameters.heroAnimation["left"]) MainHero.Image = loadParameters.heroAnimation["left"];
                    break;
                case Keys.Right:
                    gameModel.hero.MoveRight();
                    if (MainHero.Image != loadParameters.heroAnimation["right"]) MainHero.Image = loadParameters.heroAnimation["right"];
                    break;
                case Keys.Up:
                    gameModel.hero.MoveUp();
                    if (MainHero.Image != loadParameters.heroAnimation["up"]) MainHero.Image = loadParameters.heroAnimation["up"];
                    break;
                case Keys.Down:
                    gameModel.hero.MoveDown();
                    if (MainHero.Image != loadParameters.heroAnimation["down"]) MainHero.Image = loadParameters.heroAnimation["down"];
                    break;
                case Keys.Space:
                    if (loadParameters.bullet.Left > 1280 || loadParameters.bullet.Left < 0 || loadParameters.bullet.Top < 0 || loadParameters.bullet.Top > 720)
                    {
                        bulletKey = lastKey;
                        loadParameters.bullet.Location = new Point(MainHero.Location.X + MainHero.Height/2, MainHero.Location.Y + MainHero.Width/2);
                        this.Controls.Add(loadParameters.bullet);
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
            foreach(PictureBox en in loadParameters.enemyCount)
            {
                if (loadParameters.bullet.Bounds.IntersectsWith(en.Bounds))
                {
                    this.Controls.Remove(en);
                    this.Controls.Remove(loadParameters.bullet);
                    enemyKillCount++;
                    deletEnemy = en;
                }
                if (MainHero.Bounds.IntersectsWith(en.Bounds))
                {
                    if(count - invulnerability > 100)
                    {
                        liveCount--;
                        GetHP();
                        invulnerability = count;
                    }
                }
            }
            loadParameters.enemyCount.Remove(deletEnemy);
            if (MainHero.Bounds.IntersectsWith(loadParameters.box.Bounds))
            {
                if (loadParameters.box.Image.Equals(loadParameters.boxes["1box"])) boxCount += 1;
                if (loadParameters.box.Image.Equals(loadParameters.boxes["3box"])) boxCount += 3;
                if (loadParameters.box.Image.Equals(loadParameters.boxes["5box"])) boxCount += 5;
                this.Controls.Remove(loadParameters.box);
                loadParameters.BoxSpecifications(MainHero);
                this.Controls.Add(loadParameters.box);
            }
        }

        private void BulletMovement(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    loadParameters.bullet.Left -= bulletSpeed;
                    break;
                case Keys.Right:
                    loadParameters.bullet.Left += bulletSpeed;
                    break;
                case Keys.Down:
                    loadParameters.bullet.Top += bulletSpeed;
                    break;
                case Keys.Up:
                    loadParameters.bullet.Top -= bulletSpeed;
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
            foreach (PictureBox en in loadParameters.enemyCount)
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
            if (count % 400 == 0)
            {
                loadParameters.EnemySpawn(MainHero);
                this.Controls.Add(loadParameters.enemySprite);
            }
            if (liveCount == 0) GameOver();
            if (boxCount >= winCount) GameWon();
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
            EndOfGame.Visible = true;
            EndOfGame.Text = "Уровень пройден.";
            GameProgressTimer.Stop();
            currentLevel++;
            LevelChange();
            liveCount = 3;
            GetHP();
            boxCount = 0;
            enemyKillCount = 0;
            count = 0;
            foreach(PictureBox en in loadParameters.enemyCount)
            {
                this.Controls.Remove(en);
            }
            EndOfGame.Visible = false;
            loadParameters.enemyCount.Clear();
            GameProgressTimer.Start();
        }

        private void GameOver()
        {
            LivePicture.Image = HP._0hp;
            MainHero.Visible = false;
            EndOfGame.Visible = true;
            EndOfGame.Text = "Игра окончена.";
            GameProgressTimer.Stop();
        }
    }
}
