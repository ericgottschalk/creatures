using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Conditions;

namespace ENG.Creatures.Domain.Conditions
{
    public class BarrierCondition : ICondition
    {
        public void Resolve(Creature creature)
        {
            creature.RemoveCondition(this);
        }
    }
}
