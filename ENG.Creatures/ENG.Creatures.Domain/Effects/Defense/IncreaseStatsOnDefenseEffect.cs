using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Effects;
using System;

namespace ENG.Creatures.Domain.Effects.Defense
{
    public class IncreaseStatsOnDefenseEffect : IOnDefenseEffect
    {
        private readonly uint power;
        private readonly uint life;

        public IncreaseStatsOnDefenseEffect(string description, uint power, uint life)
        {
            this.power = power;
            this.life = life;
            Description = description;
        }

        public string Description { get; }

        public bool Negated => throw new NotImplementedException();

        public bool CanNagate() => false;

        public void Negate(Card target)
        {
            throw new NotImplementedException();
        }

        public void Resolve(Card target)
        {
            var creature = target as Creature;

            creature.Stats.IncreasePower(power);
            creature.Stats.IncreaseLife(life);
        }
    }
}
