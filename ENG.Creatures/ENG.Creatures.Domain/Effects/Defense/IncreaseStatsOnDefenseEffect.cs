using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Effects;
using ENG.Creatures.Domain.Effects.EndOfTurn;

namespace ENG.Creatures.Domain.Effects.Defense
{
    public class IncreaseStatsOnDefenseEffect : Effect, IOnDefenseEffect
    {
        private readonly uint power;
        private readonly uint life;

        public IncreaseStatsOnDefenseEffect(string description, uint power, uint life, bool untilEndOfTurn = true)
            : base(description, true)
        {
            this.power = power;
            this.life = life;
            UntilEndOfTurn = untilEndOfTurn;
        }

        public bool UntilEndOfTurn { get; private set; }

        public override void Resolve(Card target)
        {
            if (!Negated)
            {
                var creature = target as Creature;

                if (UntilEndOfTurn)
                {
                    var effect = new DecreaseStatsEndOfTurnEffect(string.Empty, power, life);

                    creature.AddEffect(effect);
                }

                creature.Stats.IncreasePower(power);
                creature.Stats.IncreaseLife(life);
            }
        }
    }
}
