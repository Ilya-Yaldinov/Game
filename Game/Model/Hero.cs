
namespace Game
{
    class Hero : Character
    {
        public Bullet bullet { get; }
        public int enemyKillCount { get; set; }
        public int boxCount { get;  set; }

        public Hero(int speed) : base(speed){ bullet = new Bullet(); }

        public void KillCountUpdate()
        {
            enemyKillCount++;
        }

        public void Shoot()
        {
            bullet.Move(Side);
        }

        public void BoxPickUp(int weight)
        {
            boxCount += weight;
        }
    }
}
