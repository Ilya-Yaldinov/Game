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
        private int playerSpeed;
        private GameModel gameModel;
        private EventHandler action;
        private Dictionary<string, Image> heroAnimation = new Dictionary<string, Image>();
        private Dictionary<string, Image> enemy = new Dictionary<string, Image>();
        private Dictionary<string, Image> backgrounds = new Dictionary<string, Image>();
        Panel panel = new Panel();
        private Keys lastKey = Keys.None;

        public GameBackGround()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            LoadAnimations();
            LoadBackGround();
            LoadEnemy();
            //GameBackGround.ActiveForm.BackgroundImage = backgrounds["lvl_1"];
            MainHero.Image = heroAnimation["r_d"];
            playerSpeed = 7;
            gameModel = new GameModel(playerSpeed);
            gameModel.hero.location = MainHero.Location;
            gameModel.hero.OnMove += location => 
            { 
                MainHero.Location = location;
            };
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
            //heroAnimation.Add("l_u", HeroMovement.l_u);
            heroAnimation.Add("l_d", HeroMovement.l_d);
            heroAnimation.Add("r_u", HeroMovement.r_u);
            heroAnimation.Add("r_d", HeroMovement.r_d);
        }

        private void MoveTimer_Tick(Keys key)
        {
            switch (key)
            {
                case Keys.Left:
                    lastKey = key;
                    gameModel.hero.MoveLeft();
                    if (MainHero.Image != heroAnimation["l_d"]) MainHero.Image = heroAnimation["l_d"]; 
                    break;
                case Keys.Right:
                    lastKey = key;
                    gameModel.hero.MoveRight();
                    if (MainHero.Image != heroAnimation["r_d"]) MainHero.Image = heroAnimation["r_d"];
                    break;
                case Keys.Up:
                    gameModel.hero.MoveUp();
                    if (MainHero.Image != heroAnimation["r_u"]) MainHero.Image = heroAnimation["r_u"];
                    break;
                case Keys.Down:
                    gameModel.hero.MoveDown();
                    if (lastKey == Keys.Left)
                    {
                        if (MainHero.Image != heroAnimation["r_d"]) MainHero.Image = heroAnimation["r_d"];
                    }
                    else
                    {
                        if (MainHero.Image != heroAnimation["l_d"]) MainHero.Image = heroAnimation["l_d"];
                    }
                    break;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)//TODO прописать анимации остановки
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
    }
}
