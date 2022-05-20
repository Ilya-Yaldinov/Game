using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game
{
    static class LoadBoxes
    {
        public static readonly Image box1 = Resources.BoxPicture._1box;
        public static readonly Image box3 = Resources.BoxPicture._3box;
        public static readonly Image box5 = Resources.BoxPicture._5box;
        public static List<Image> images = new List<Image>()
        {
                box1, box1, box1, box1, box1, box1,
                box3, box3, box3,
                box5
        };

        public static Image GetBoxImage()
        {
            return images.ElementAt(new Random().Next(0, 10));
        }
    }
}
