namespace CounterStrike.Models.Guns
{
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Utilities.Messages;
    using CounterStrike.Utilities.Validators;
    using System;

    public abstract class Gun : IGun
    {
        private string name;
        private int bulletsCount;

        protected Gun(string name, int bulletsCount)
        {
            Name = name;
            BulletsCount = bulletsCount;
        }

        public string Name
        {
            get => name;
            private set
            {
                DataValidator.ThrowArgumentExceptionIfStringValueIsEmpty(value, ExceptionMessages.InvalidGunName);
                name = value;
            }
        }

        public int BulletsCount
        {
            get => bulletsCount;
            protected set
            {
                DataValidator.ThrowArgumentExceptionIfIntegerValueIsBelowZero(value, ExceptionMessages.InvalidGunBulletsCount);
                bulletsCount = value;
            }
        }

        public abstract int Fire();
    }
}
