using System.IO;
using System.Media;

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
