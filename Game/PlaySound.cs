using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class PlaySound
    {
        private static readonly SoundPlayer player = new SoundPlayer();
        public static void Play(Stream sound)
        {
            player.Stream = sound;
            player.Play();
        }
    }
}
