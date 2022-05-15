namespace Game
{
    class GameModel
    {
        /*public int sizeX { get; private set; } = 1280;
        public int sizeY { get; private set; } = 720;*/
        public Hero hero { get; private set; }

        public GameModel(int Speed)
        {
            hero = new Hero(Speed);
        }
    }
}
