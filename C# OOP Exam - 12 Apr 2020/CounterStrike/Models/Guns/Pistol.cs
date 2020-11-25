namespace CounterStrike.Models.Guns
{
    using CounterStrike.Models.Guns.Contracts;
    public class Pistol : Gun, IGun
    {
        public Pistol(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int bulletsFired = 0;
            if (BulletsCount > 0)
            {
                BulletsCount--;
                bulletsFired++;
            }
            return bulletsFired;
        }
    }
}
