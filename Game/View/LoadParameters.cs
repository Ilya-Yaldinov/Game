using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    static class LoadParameters
    {
        public static PictureBox bullet { get; set; }
        public static PictureBox box { get; set; }
        public static PictureBox enemySprite { get; set; }
        public static Image enemyImage { get; set; }
        public static List<PictureBox> enemyCount = new List<PictureBox>();

        public static void BulletSpecifications()
        {
            bullet = new PictureBox();
            bullet.BorderStyle = BorderStyle.None;
            bullet.Size = new Size(10, 10);
            bullet.BackColor = Color.Red;
            Bullet bulletModel = new Bullet();
            bullet.DataBindings.Add("", bulletModel, "");
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
            int weight;
            if (box.Image.Equals(LoadBoxes.box1)) weight = 1;
            else if (box.Image.Equals(LoadBoxes.box3)) weight = 3;
            else weight = 5;
            Box boxModel = new Box(weight);
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
