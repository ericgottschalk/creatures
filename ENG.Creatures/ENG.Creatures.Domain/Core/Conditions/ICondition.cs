using ENG.Creatures.Domain.Core.Cards;

namespace ENG.Creatures.Domain.Core.Conditions
{
    public interface ICondition
    {
        void Resolve(Creature creature);
    }
}
