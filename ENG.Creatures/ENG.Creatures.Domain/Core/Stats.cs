using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Creatures.Domain.Core
{
    public class Stats
    {
        private uint originalPower;
        private uint originalLife;

        public Stats(uint power, uint life)
        {
            ValideteContruction(power, life);

            originalPower = power;
            Power = power;
            originalLife = life;
            Life = life;
        }

        public uint Power { get; private set; }

        public uint Life { get; private set; }

        public bool Damaged { get; private set; }

        public void IncreasePower(uint value) => Power += value;

        public void DecreasePower(uint value) 
        {
            if (Power < value)
                Power = 0;

            Power -= value;
        }

        public void IncreaseLife(uint value) => Life += value;

        public void DecreaseLife(uint value)
        {
            if (Life < value)
                Life = 0;

            Life -= value;
            Damaged = true;
        }

        public void Heal()
        {
            if (Damaged)
            {
                Life = originalLife;
                Damaged = false;
            }
        }

        public void SetDefaultPower(uint power)
        {
            if (power < 0)
                throw new ArgumentException(nameof(power));

            originalPower = power;
        }

        public void SetDefaultLife(uint life)
        {
            if (life < 0)
                throw new ArgumentException(nameof(life));

            originalPower = life;
        }

        public void Reset()
        {
            ResetPower();
            ResetLife();
        }

        public void ResetPower() => Power = originalPower;

        public void ResetLife() => Life = originalLife;

        private void ValideteContruction(uint power, uint life)
        {
            if (power < 0)
                throw new ArgumentException(nameof(power));

            if (life <= 0)
                throw new ArgumentException(nameof(life));
        }
    }
}
