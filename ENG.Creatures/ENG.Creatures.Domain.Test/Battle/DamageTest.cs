using ENG.Creatures.Domain.Conditions;
using ENG.Creatures.Domain.Core.Cards;
using ENG.Creatures.Domain.Core.Conditions;
using ENG.Creatures.Domain.Effects.Attack;
using ENG.Creatures.Domain.Effects.Defense;
using NUnit.Framework;

namespace ENG.Creatures.Domain.Test.Battle
{
    [TestFixture]
    public class DamageTest
    {
        private Creature attacker;
        private Creature defensor;
        private BarrierCondition barrierCondition;
        private uint attackerExpectedLife;
        private uint defensorExpectedLife;

        [SetUp]
        public void Setup()
        {
            attacker = new Creature("attacker", 0, Core.Rarity.Common, new Core.Stats(10, 5));
            defensor = new Creature("defensor", 0, Core.Rarity.Common, new Core.Stats(4, 11));
            barrierCondition = new BarrierCondition();
            defensorExpectedLife = defensor.Stats.Life - attacker.Stats.Power;
            attackerExpectedLife = attacker.Stats.Life - defensor.Stats.Power;
        }

        [Test]
        public void Damage_Attack_Defense_Test()
        {
            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLife, defensor.Stats.Life);
            Assert.AreEqual(attackerExpectedLife, attacker.Stats.Life);
        }

        [Test]
        public void Damage_IncreasePower_AttackEffect_Test()
        {
            var effect = new IncreaseStatsOnAttackEffect("Add +1/+0 on attack", 1, 0);
            attacker.AddEffect(effect);

            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(attackerExpectedLife, attacker.Stats.Life);
            Assert.False(defensor.IsAlive());
        }

        [Test]
        public void Damage_IncreaseLife_AttackEffect_Test()
        {
            uint increasedDefense = 4;
            var attackerExpectedLifeWithIncrease = attackerExpectedLife + increasedDefense;

            var effect = new IncreaseStatsOnAttackEffect("Add +1/+5 on attack", 0, increasedDefense);
            attacker.AddEffect(effect);

            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLife, defensor.Stats.Life);
            Assert.AreEqual(attackerExpectedLifeWithIncrease, attacker.Stats.Life);
        }

        [Test]
        public void Damage_IncreasePower_DefenseEffect_Defense_Test()
        {
            var effect = new IncreaseStatsOnDefenseEffect("Add +1/+0 on defense", 1, 0);
            defensor.AddEffect(effect);

            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLife, defensor.Stats.Life);
            Assert.False(attacker.IsAlive());
        }

        [Test]
        public void Damage_IncreaseLife_DefenseEffect_Defense_Test()
        {
            uint increasedDefense = 10;
            var defensorExpectedLifeWithIncrease = defensorExpectedLife + increasedDefense;

            var effect = new IncreaseStatsOnDefenseEffect("Add +0/+10 on defense", 0, increasedDefense);
            defensor.AddEffect(effect);

            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLifeWithIncrease, defensor.Stats.Life);
            Assert.AreEqual(attackerExpectedLife, attacker.Stats.Life);
        }

        [Test]
        public void Demage_BarrierCondition_Attacker_Test()
        {
            var attackerExpectedLifeWithBarrier = attacker.Stats.Life;
            attacker.AddCondition(barrierCondition);

            attacker.Attack(defensor);

            Assert.IsFalse(attacker.Stats.Damaged);
            Assert.IsTrue(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLife, defensor.Stats.Life);
            Assert.AreEqual(attackerExpectedLifeWithBarrier, attacker.Stats.Life);
        }

        [Test]
        public void Demage_BarrierCondition_Defensor_Test()
        {
            var defensorExpectedLifeWithBarrier = defensor.Stats.Life;
            defensor.AddCondition(barrierCondition);

            attacker.Attack(defensor);

            Assert.IsTrue(attacker.Stats.Damaged);
            Assert.IsFalse(defensor.Stats.Damaged);
            Assert.AreEqual(defensorExpectedLifeWithBarrier, defensor.Stats.Life);
            Assert.AreEqual(attackerExpectedLife, attacker.Stats.Life);
        }
    }
}
