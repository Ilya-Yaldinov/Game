using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    class LoadParameters
    {
        public PictureBox bullet = null;
        public PictureBox box = null;
        public PictureBox enemySprite = null;
        public Image enemyImage = null;
        public List<PictureBox> enemyCount = new List<PictureBox>();
        public Dictionary<string, Image> heroAnimation = new Dictionary<string, Image>();
        public Dictionary<string, Image> boxes = new Dictionary<string, Image>();

        public void Start(PictureBox mainHero)
        {
            LoadAnimations();
            LoadBoxes();
            BulletSpecifications();
            BoxSpecifications(mainHero);
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

        private Image GetBoxImage()
        {
            List<Image> images = new List<Image>();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                images.Add(boxes["1box"]);
                if (i < 3) images.Add(boxes["3box"]);
            }
            images.Add(boxes["5box"]);

            return images.ElementAt(random.Next(0, 10));
        }

        private void BulletSpecifications()
        {
            bullet = new PictureBox();
            bullet.BorderStyle = BorderStyle.None;
            bullet.Size = new Size(10, 10);
            bullet.BackColor = Color.Red;
        }

        public void BoxSpecifications(PictureBox mainHero)
        {
            Random random = new Random();
            box = new PictureBox();
            box.Image = GetBoxImage();
            box.Size = new Size(mainHero.Height / 2, mainHero.Width / 2);
            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.BackColor = Color.Transparent;
            box.Location = new Point(random.Next(10, 1000), random.Next(10, 600));


        }

        public void EnemySpawn(PictureBox mainHero)
        {
            enemySprite = new PictureBox();
            enemySprite.Image = enemyImage;
            enemySprite.Size = new Size(mainHero.Height, mainHero.Width);
            enemySprite.SizeMode = PictureBoxSizeMode.Zoom;
            enemySprite.BackColor = Color.Transparent;
            enemySprite.Location = new Point(640, 360);
            enemyCount.Add(enemySprite);
        }
    }
}
