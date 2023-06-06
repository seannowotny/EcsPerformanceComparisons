using DefaultEcs;
using Logic.DefaultECS;
using Logic.DefaultECS.Components;
using NUnit.Framework;
using Unity.Mathematics;

namespace Pisces.Tests.DefaultECS
{
    public class ShootSystemTests
    {
        private World world;
        private ShootSystem system;

        [SetUp]
        public void Setup()
        {
            world = new World();
            system = new ShootSystem(world);
        }

        [Test]
        public void Test_NoDamage_IfOutOfRange()
        {
            var shooter = world.CreateEntity();
            shooter.Set(new PositionDC { Value = new float2(0, 0) });

            var target = world.CreateEntity();
            target.Set(new PositionDC { Value = new float2(Data.WeaponRange + 1, 0) });
            target.Set(new HealthDC { Value = 100 });

            shooter.Set(new TargetDC { Value = target });

            system.Update(TestData.DeltaTime);

            var health = target.Get<HealthDC>().Value;
            Assert.AreEqual(100, health);
        }

        [Test]
        public void Test_Damage_IfInRange()
        {
            var shooter = world.CreateEntity();
            shooter.Set(new PositionDC { Value = new float2(0, 0) });

            var target = world.CreateEntity();
            target.Set(new PositionDC { Value = new float2(Data.WeaponRange - 1, 0) });
            target.Set(new HealthDC { Value = 100 });

            shooter.Set(new TargetDC { Value = target });

            system.Update(TestData.DeltaTime);

            var health = target.Get<HealthDC>().Value;
            Assert.IsTrue(math.abs((100 - Data.WeaponDamage * TestData.DeltaTime) - health) < TestData.ErrorTolerance);
        }

        [TearDown]
        public void TearDown()
        {
            system.Dispose();
            world.Dispose();
        }
    }
}