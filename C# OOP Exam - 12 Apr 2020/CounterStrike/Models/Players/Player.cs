namespace CounterStrike.Models.Players
{
    using CounterStrike.Models.Guns.Contracts;
    using CounterStrike.Models.Players.Contracts;
    using CounterStrike.Utilities.Messages;
    using CounterStrike.Utilities.Validators;
    using System;
    using System.Text;

    public abstract class Player : IPlayer
    {
        private string userName;
        private int health;
        private int armor;
        private IGun gun;

        protected Player(string username, int health, int armor, IGun gun)
        {
            Username = username;
            Health = health;
            Armor = armor;
            Gun = gun;
        }

        public string Username
        {
            get => userName;
            private set
            {
                DataValidator.ThrowArgumentExceptionIfStringValueIsEmpty(value, ExceptionMessages.InvalidPlayerName);
                userName = value;
            }
        }

        public int Health
        {
            get => health;
            private set
            {
                DataValidator.ThrowArgumentExceptionIfIntegerValueIsBelowZero(value, ExceptionMessages.InvalidPlayerHealth);
                health = value;
            }
        }

        public int Armor
        {
            get => armor;
            private set
            {
                DataValidator.ThrowArgumentExceptionIfIntegerValueIsBelowZero(value, ExceptionMessages.InvalidPlayerArmor);
                armor = value;
            }
        }


        public IGun Gun
        {
            get => gun;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunName);
                }
                gun = value;
            }
        }

        public bool IsAlive
            => Health > 0;

        public void TakeDamage(int points)
        {
            if (Armor >= points)
            {
                Armor -= points;
            }
            else
            {
                points -= Armor;
                Armor = 0;
                if (Health >= points)
                {
                    Health -= points;
                }
                else
                {
                    Health = 0;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {Username}");
            sb.AppendLine($"--Health: {Health}");
            sb.AppendLine($"--Armor: {Armor}");
            sb.AppendLine($"--Gun: {Gun.Name}");

            return sb.ToString().TrimEnd();
        }
    }
}
