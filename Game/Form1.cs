using Game.Resources;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Game
{
    public partial class GameBackGround : Form
    {
        private GameModel gameModel;
        private EventHandler action;
        
        private const int gameSpeed = 7;
        private int count = 0;
        private int invulnerability = 0;
        private int enemyKillCount = 0;
        private int boxCount = 0;
        private int liveCount = 3;
        private int winCount = 25;
        private int currentLevel = 1;
        private Keys lastKey = Keys.None;
        private Keys bulletKey = Keys.Right;
        
        public GameBackGround()
        {
            InitializeComponent();
            gameModel = new GameModel(gameSpeed);
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            LoadParameters.Start(MainHero);
            Controls.Add(LoadParameters.box);
            LevelChange();
            GetHP();
            MainHero.Image = HeroMoveAnimations.right;
            gameModel.hero.location = MainHero.Location;
            MainHero.DataBindings.Add(new Binding("", gameModel.hero, ""));
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
                    BackgroundImage = GameBG.lvl_1;
                    LoadParameters.enemyImage = EnemyMovement.spider;
                    break;
                case 2:
                    BackgroundImage = GameBG.lvl_2;
                    LoadParameters.enemyImage = EnemyMovement.skelet;
                    break;
                case 3:
                    BackgroundImage = GameBG.lvl_3;
                    LoadParameters.enemyImage = EnemyMovement.ghost;
                    break;
                case 4:
                    BackgroundImage = GameBG.lvl_4;
                    LoadParameters.enemyImage = EnemyMovement.zombi;
                    break;
                case 5:
                    BackgroundImage = GameBG.lvl_5;
                    LoadParameters.enemyImage = EnemyMovement.eye;
                    break;
                case 6:
                    EndOfGame.Visible = true;
                    EndOfGame.Text = "Вы победили";
                    GameProgressTimer.Stop();
                    break;
            }
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void HeroMove(Keys key)
        {
            if(key != Keys.Space) lastKey = key;
            switch (key)
            {
                case Keys.Left:
                    gameModel.hero.MoveLeft();
                    if (MainHero.Image != HeroMoveAnimations.left) MainHero.Image = HeroMoveAnimations.left;
                    break;
                case Keys.Right:
                    gameModel.hero.MoveRight();
                    if (MainHero.Image != HeroMoveAnimations.right) MainHero.Image = HeroMoveAnimations.right;
                    break;
                case Keys.Up:
                    gameModel.hero.MoveUp();
                    if (MainHero.Image != HeroMoveAnimations.up) MainHero.Image = HeroMoveAnimations.up;
                    break;
                case Keys.Down:
                    gameModel.hero.MoveDown();
                    if (MainHero.Image != HeroMoveAnimations.down) MainHero.Image = HeroMoveAnimations.down;
                    break;
                case Keys.Space:
                    if (LoadParameters.bullet.Left > 1280 || LoadParameters.bullet.Left < 0 || LoadParameters.bullet.Top < 0 || LoadParameters.bullet.Top > 720)
                    {
                        PlaySound.Play(Sounds.shoot);
                        bulletKey = lastKey;
                        LoadParameters.bullet.Location = new Point(MainHero.Location.X + MainHero.Height/2, MainHero.Location.Y + MainHero.Width/2);
                        Controls.Add(LoadParameters.bullet);
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

        private void Interspect()
        {
            PictureBox deletEnemy = null;
            foreach(PictureBox en in LoadParameters.enemyCount)
            {
                if (LoadParameters.bullet.Bounds.IntersectsWith(en.Bounds))
                {
                    PlaySound.Play(Sounds.hitEnemy);
                    Controls.Remove(en);
                    Controls.Remove(LoadParameters.bullet);
                    enemyKillCount++;
                    deletEnemy = en;
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
            LoadParameters.enemyCount.Remove(deletEnemy);
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

        

        

        private void GameProgressTimer_Tick(object sender, EventArgs e)
        {
            Interspect();
            KillLable.Text = $"Противников убито: {enemyKillCount}";
            BoxCount.Text = $"Коробочек собранно: {boxCount}";
            count++;
            Console.WriteLine(count);
            BulletMovement(bulletKey);
            if (count % 10 == 0)
            {
                foreach(var child in Controls)
                {
                    if (child is PictureBox box)
                        if(box.DataBindings.Count != 0)
                            if (box.DataBindings[0].DataSource is Enemy enemy)
                                enemy.Move();
                }
            }
            if (count % 400 == 0)
            {
                LoadParameters.EnemySpawn(MainHero);
                Controls.Add(LoadParameters.enemySprite);
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
            Keys isEnter = new Keys();
            PlaySound.Play(Sounds.win);
            EndOfGame.Visible = true;
            EndOfGame.Text = "Уровень пройден.";
            while (isEnter != Keys.Enter) ;
            GameProgressTimer.Stop();
            currentLevel++;
            invulnerability = 0;
            LevelChange();
            liveCount = 3;
            GetHP();
            boxCount = 0;
            enemyKillCount = 0;
            count = 0;
            foreach(PictureBox en in LoadParameters.enemyCount)
            {
                Controls.Remove(en);
            }
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
