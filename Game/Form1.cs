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
        private int invulnerability = 0;
        private int count = 0;
        private int winCount = 25;

        public GameBackGround()
        {
            InitializeComponent();
            gameModel = new GameModel();
            GetHP();
            LevelChange();
            gameModel.Hero.OnMove += location =>
            {
                MainHero.Location = location;
            };
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Sprite_Load(object sender, EventArgs e)
        {
            LoadParameters.BulletSpecifications();
            LoadParameters.BoxSpecifications(MainHero);
            Controls.Add(LoadParameters.box);
            MainHero.Image = HeroMoveAnimations.right;
            gameModel.Hero.location = MainHero.Location;
            MainHero.DataBindings.Add(new Binding("", gameModel.Hero, ""));
        }

        public void Update()
        {
            PictureBox deleteEnemy = null;
            foreach (PictureBox en in LoadParameters.enemyCount)
            {
                if (LoadParameters.bullet.Bounds.IntersectsWith(en.Bounds))
                {
                    PlaySound.Play(Sounds.hitEnemy);
                    Controls.Remove(LoadParameters.bullet);
                    gameModel.Hero.KillCountUpdate();
                    Controls.Remove(en);
                    deleteEnemy = en;
                }
                if (MainHero.Bounds.IntersectsWith(en.Bounds))
                {
                    if(count - invulnerability > 100)
                    {
                        invulnerability = count;
                        PlaySound.Play(Sounds.hitHeart);
                        gameModel.Hit();
                        GetHP();
                    }
                }
            }
            LoadParameters.enemyCount.Remove(deleteEnemy);
        }

        private void GetHP()
        {
            LivePicture.SizeMode = PictureBoxSizeMode.Zoom;
            switch (gameModel.liveCount)
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

        private void LevelChange()
        {
            switch (gameModel.currentLevel)
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

        public void Interspect()
        {
            if (MainHero.Bounds.IntersectsWith(LoadParameters.box.Bounds))
            {
                gameModel.PickBox();
                PlaySound.Play(Sounds.pickupBox);
                Controls.Remove(LoadParameters.box);
                LoadParameters.BoxSpecifications(MainHero);
                Controls.Add(LoadParameters.box);
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            MoveTimer.Start();
            MoveTimer.Tick -= action;
            action = (obj, args) => gameModel.HeroMove(e.KeyCode, MainHero);
            MoveTimer.Tick += action;
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            MoveTimer.Stop();
        }

        private void GameProgressTimer_Tick(object sender, EventArgs e)
        {
            Interspect();
            Update();
            KillLable.Text = $"Противников убито: {gameModel.Hero.enemyKillCount}";
            BoxCount.Text = $"Коробочек собранно: {gameModel.Hero.boxCount}";
            count++;
            if(gameModel.Hero.bullet.IsExist)
            {
                if (!(LoadParameters.bullet.Left > 1280 || LoadParameters.bullet.Left < 0 || LoadParameters.bullet.Top < 0 || LoadParameters.bullet.Top > 720))
                {
                    gameModel.Hero.Shoot();
                }
                else
                {
                    PlaySound.Play(Sounds.shoot);
                    LoadParameters.bullet.Location = new Point(MainHero.Location.X + MainHero.Height / 2, MainHero.Location.Y + MainHero.Width / 2);
                    gameModel.Hero.bullet.IsExist = false;
                    Controls.Add(LoadParameters.bullet);
                }
            }
            
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
            if (gameModel.liveCount == 0) GameOver();
            if (gameModel.Hero.boxCount >= winCount) GameWon();
        }

        private void GameWon()//пауза до нажатия enter
        {
            PlaySound.Play(Sounds.win);
            EndOfGame.Visible = true;
            EndOfGame.Text = "Уровень пройден.";
            GameProgressTimer.Stop();
            gameModel.NextLevel();
            LevelChange();
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
