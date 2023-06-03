using NUnit.Framework;
using DefaultEcs;
using Logic.DefaultECS;
using Logic.DefaultECS.Components;
using System.Linq;
using Pisces.Tests;

namespace Tests
{
    public class EnemyTargetSystemTests
    {
        private World world;
        private EnemyTargetSystem system;

        [SetUp]
        public void Setup()
        {
            world = new World();
            system = new EnemyTargetSystem(world);
        }

        [Test]
        public void Test_TargetNotChanged_IfTargetIsAlive()
        {
            var enemy = world.CreateEntity();
            enemy.Set(new TeamDC() {Value = 1});

            var target = world.CreateEntity();
            target.Set(new TeamDC() {Value = 2});
            target.Set(new TargetDC() {Value = enemy});

            system.Update(TestData.DeltaTime);

            var newTarget = target.Get<TargetDC>().Value;
            Assert.AreEqual(enemy, newTarget);
        }

        [Test]
        public void Test_TargetNotChanged_IfNoAliveEnemyTeam()
        {
            var target = world.CreateEntity();
            target.Set(new TeamDC() {Value = 1});
            target.Set(new TargetDC() {Value = new Entity()});

            system.Update(TestData.DeltaTime);

            var newTarget = target.Get<TargetDC>().Value;
            Assert.IsFalse(newTarget.IsAlive);
        }

        [Test]
        public void Test_NewTargetSet_IfAliveEnemyTeam()
        {
            var enemyTeam = Enumerable.Range(0, 3)
                                      .Select(i => {
                                            var e = world.CreateEntity();
                                            e.Set(new TeamDC() { Value = 1 });
                                            return e;
                                      })
                                      .ToArray();

            var target = world.CreateEntity();
            target.Set(new TeamDC() {Value = 2});
            target.Set(new TargetDC() {Value = new Entity()});

            system.Update(TestData.DeltaTime);

            var newTarget = target.Get<TargetDC>().Value;
            Assert.IsTrue(newTarget.IsAlive);
            Assert.IsTrue(enemyTeam.Contains(newTarget));
        }

        [TearDown]
        public void TearDown()
        {
            system.Dispose();
            world.Dispose();
        }
    }
}
