using ENG.Creatures.Domain.Core.Cards;

namespace ENG.Creatures.Domain.Core.Effects
{
    public abstract class Effect : IEffect
    {
        public Effect(string description, bool canNagate)
        {
            Description = description;
            CanNagate = canNagate;
        }

        public string Description { get; }

        public bool Negated { get; private set; }

        public bool CanNagate { get; }

        public void Negate(Card target)
        {
            Negated = CanNagate;
        }

        public abstract void Resolve(Card target);
    }
}
