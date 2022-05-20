using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class LoadBoxes
    {
        public static readonly Image box1 = Resources.BoxPicture._1box;
        public static readonly Image box3 = Resources.BoxPicture._3box;
        public static readonly Image box5 = Resources.BoxPicture._5box;

        public static Image GetBoxImage()
        {
            List<Image> images = new List<Image>();
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                images.Add(box1);
                if (i < 3) images.Add(box3);
            }
            images.Add(box5);

            return images.ElementAt(random.Next(0, 10));
        }
    }
}
