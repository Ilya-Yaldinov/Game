using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    static class LoadParameters
    {
        public static PictureBox bullet { get; set; }
        public static PictureBox box { get; set; }
        public static PictureBox enemySprite { get; set; }
        public static Image enemyImage { get; set; }
        public const string boxObject = "boxObject";
        public const string enemyObject = "enemyObject";
        public static List<PictureBox> enemyCount = new List<PictureBox>();

        public static void Start(PictureBox mainHero)
        {
            BulletSpecifications();
            BoxSpecifications(mainHero);
        }

        private static void BulletSpecifications()
        {
            bullet = new PictureBox();
            bullet.BorderStyle = BorderStyle.None;
            bullet.Size = new Size(10, 10);
            bullet.BackColor = Color.Red;
        }

        public static void BoxSpecifications(PictureBox mainHero)
        {
            Random random = new Random();
            box = new PictureBox();
            box.Image = LoadBoxes.GetBoxImage();
            box.Size = new Size(mainHero.Height / 2, mainHero.Width / 2);
            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.BackColor = Color.Transparent;
            box.Location = new Point(random.Next(10, 1000), random.Next(100, 600));
            Box boxModel = new Box();
            box.DataBindings.Add(new Binding("", boxModel, ""));
        }

        public static void EnemySpawn(PictureBox mainHero)
        {
            enemySprite = new PictureBox();
            enemySprite.Image = enemyImage;
            enemySprite.Size = new Size(mainHero.Height, mainHero.Width);
            enemySprite.SizeMode = PictureBoxSizeMode.Zoom;
            enemySprite.BackColor = Color.Transparent;
            enemySprite.Location = new Point(640, 360);
            enemyCount.Add(enemySprite);
            Enemy enemyModel = new Enemy(((Hero)mainHero.DataBindings[0].DataSource).speed);
            enemySprite.DataBindings.Add(new Binding("", enemyModel, ""));
        }
    }
}
