using ENG.Creatures.Domain.Core.Effects;
using System.Collections.Generic;
using System.Linq;

namespace ENG.Creatures.Domain.Core.Cards
{
    public abstract class Card
    {
        public Card(string name, int cost, Rarity rarity)
        {
            Name = name;
            Cost = cost;
            Rarity = rarity;
            Effects = new List<IEffect>();
        }

        public string Name { get; }

        public Rarity Rarity { get; }

        public int Cost { get; set; }

        public ICollection<IEffect> Effects { get; private set; }

        public void AddEffect(IEffect effect) => Effects.Add(effect);

        public bool HasEffect() => Effects.Count > 0;

        public bool HasOnAttackEffect => Effects.Any(t => t is IOnAttackEffect);

        public bool HasOnDefenseEffect => Effects.Any(t => t is IOnDefenseEffect);
    }
}
