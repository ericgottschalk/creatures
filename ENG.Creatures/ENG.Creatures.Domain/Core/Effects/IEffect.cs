using ENG.Creatures.Domain.Core.Cards;

namespace ENG.Creatures.Domain.Core
{
    public interface IEffect
    {
        string Description { get; }

        bool Negated { get; }

        void Resolve(Card target);

        void Negate(Card target);

        bool CanNagate();
    }
}
