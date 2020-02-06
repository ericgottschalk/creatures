using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Creatures.Domain.Effects.EndOfTurn
{
    public class SetStatsEndOfTurnEffect : IEndOfTurnEffect
    {
        private readonly uint power;
        private readonly uint life;
        private readonly bool canNegate;

        public SetStatsEndOfTurnEffect(string description, uint power, uint life, bool canNegate = true)
        {
            this.power = power;
            this.life = life;
            this.canNegate = canNegate;
            Description = description;
        }

        public string Description { get; }

        public bool Negated { get; private set; }

        public bool CanNagate() => canNegate;

        public void Negate(Card target)
        {
            if (CanNagate())
                Negated = true;
        }

        public void Resolve(Card target)
        {
            if (!Negated)
            {
                var creature = target as Creature;

                if (creature.Stats.Power > power)
                    creature.Stats.DecreasePower(creature.Stats.Power - power);

                if (creature.Stats.Life > life)
                    creature.Stats.DecreasePower(creature.Stats.Life - life);
            }
        }
    }
}
