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
        private int winCount = 25;
        private int currentLevel = 1;
        
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
            LoadParameters.BulletSpecifications();
            LoadParameters.BoxSpecifications(MainHero);
            Controls.Add(LoadParameters.box);
            LevelChange();
            GetHP();
            MainHero.Image = HeroMoveAnimations.right;
            gameModel.Hero.location = MainHero.Location;
            MainHero.DataBindings.Add(new Binding("", gameModel.Hero, ""));
            gameModel.Hero.OnMove += location =>
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
            gameModel.Interspect();
            KillLable.Text = $"Противников убито: {enemyKillCount}";
            BoxCount.Text = $"Коробочек собранно: {boxCount}";
            count++;
            gameModel.Hero.Shoot();
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
    }
}
