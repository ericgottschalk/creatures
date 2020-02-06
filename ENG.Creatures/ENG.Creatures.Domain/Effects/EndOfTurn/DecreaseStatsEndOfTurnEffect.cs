using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Effects;

namespace ENG.Creatures.Domain.Effects.EndOfTurn
{
    public class DecreaseStatsEndOfTurnEffect : Effect, IEndOfTurnEffect
    {
        private readonly uint power;
        private readonly uint life;
        private readonly bool canNegate;

        public DecreaseStatsEndOfTurnEffect(string description, uint power, uint life)
            : base(description, true)
        {
            this.power = power;
            this.life = life;
        }

        public override void Resolve(Card target)
        {
            if (!Negated)
            {
                var creature = target as Creature;

                if (creature.Stats.PowerGreaterThanOriginal)
                    creature.Stats.DecreasePower(power);

                if (creature.Stats.LifeGreaterThanOriginal)
                    creature.Stats.DecreasePower(life);
            }
        }
    }
}
