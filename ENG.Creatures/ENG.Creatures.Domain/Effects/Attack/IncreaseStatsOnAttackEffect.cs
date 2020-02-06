using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Effects;
using System;

namespace ENG.Creatures.Domain.Effects.Attack
{
    public class IncreaseStatsOnAttackEffect : IOnAttackEffect
    {
        private readonly uint power;
        private readonly uint life;

        public IncreaseStatsOnAttackEffect(string description, uint power, uint life, bool untilEndOfTurn = true)
        {
            this.power = power;
            this.life = life;
            Description = description;
            UntilEndOfTurn = untilEndOfTurn;
        }

        public string Description { get; }

        public bool UntilEndOfTurn { get; private set; }

        public bool Negated => false;

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
