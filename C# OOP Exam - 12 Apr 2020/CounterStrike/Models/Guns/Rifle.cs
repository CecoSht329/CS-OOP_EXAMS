
namespace CounterStrike.Models.Guns
{
    using CounterStrike.Models.Guns.Contracts;
    public class Rifle : Gun, IGun
    {
        public Rifle(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            int bulletsFired = 0;
            if (BulletsCount > 0 && BulletsCount % 10 == 0)
            {
                BulletsCount -= 10;
                bulletsFired += 10;
            }
            return bulletsFired;
        }
    }
}
