using DefaultEcs;
using NUnit.Framework;
using Logic.DefaultECS;
using Logic.DefaultECS.Components;
using Pisces.Tests;

namespace Tests
{
    public class DieSystemTests
    {
        private World world;
        private DieSystem dieSystem;

        [SetUp]
        public void Setup()
        {
            world = new World();
            dieSystem = new DieSystem(world);
        }

        [Test]
        public void DieSystem_Update_ShouldDisposeDeadEntities()
        {
            // Arrange
            var livingEntity = world.CreateEntity();
            var deadEntity = world.CreateEntity();

            livingEntity.Set(new HealthDC { Value = 50 });
            deadEntity.Set(new HealthDC { Value = 0 });

            // Act
            dieSystem.Update(TestData.DeltaTime);

            // Assert
            Assert.True(livingEntity.IsAlive);
            Assert.False(deadEntity.IsAlive);
        }

        [TearDown]
        public void TearDown()
        {
            dieSystem.Dispose();
            world.Dispose();
        }
    }
}