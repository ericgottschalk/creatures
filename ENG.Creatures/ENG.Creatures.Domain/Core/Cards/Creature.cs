using ENG.Creatures.Domain.Conditions;
using ENG.Creatures.Domain.Core.Conditions;
using ENG.Creatures.Domain.Core.Effects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ENG.Creatures.Domain.Core.Cards
{
    public class Creature : Card
    {
        public Creature(string name, int cost, Rarity rarity, Stats stats) 
            : base(name, cost, rarity)
        {
            Stats = stats;
            Conditions = new List<ICondition>();
        }

        public Stats Stats { get; private set; }

        public ICollection<ICondition> Conditions { get; private set; }

        public void Attack(Creature defensor)
        {
            if (HasOnAttackEffect)
            {
                var effect = Effects.Single(t => t is IOnAttackEffect);

                effect.Resolve(this);
            }

            defensor.Defense(this);
        }

        private void Defense(Creature attacker)
        {
            if (HasOnDefenseEffect)
            {
                var effect = Effects.Single(t => t is IOnDefenseEffect);

                effect.Resolve(this);
            }

            ResolveBattleDamage(attacker, this);
        }

        public void AddCondition(ICondition condition)
        {
            if (Conditions.Any(t => t.GetType() == condition.GetType()))
            {
                return;
            }

            Conditions.Add(condition);
        }

        public void RemoveCondition(ICondition condition)
        {
            Conditions.Remove(condition);
        }

        public void ResolveEffect(Type type)
        {
            var effect = Effects.SingleOrDefault(t => t.GetType() == type);

            effect?.Resolve(this);
        }

        public bool IsAlive() => Stats.Life > 0;

        public bool CanTakeDamage()
        {
            var barrier = Conditions.SingleOrDefault(t => t is BarrierCondition);

            if (barrier != null)
            {
                barrier.Resolve(this);
                return false;
            }

            return true;
        }

        public void ResolveDamage(uint damage)
        {
            if (CanTakeDamage())
            {
                Stats.DecreaseLife(damage);
            }
        }

        private void ResolveBattleDamage(Creature attacker, Creature defensor)
        {
            if (attacker.CanTakeDamage())
            {
                attacker.Stats.DecreaseLife(defensor.Stats.Power);

                defensor.ResolveEffect(typeof(IOnDamageCausedEffect));
                attacker.ResolveEffect(typeof(IOnDamageTakenEffect));
            }

            if (defensor.CanTakeDamage())
            {
                defensor.Stats.DecreaseLife(attacker.Stats.Power);

                attacker.ResolveEffect(typeof(IOnDamageCausedEffect));
                defensor.ResolveEffect(typeof(IOnDamageTakenEffect));
            }
        }
    }
}
