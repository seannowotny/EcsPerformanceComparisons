using NUnit.Framework;
using DefaultEcs;
using Logic.DefaultECS;
using Logic.DefaultECS.Components;
using Pisces.Tests;

namespace Tests
{
    public class SpawnVehiclesSystemTests
    {
        private World world;
        private SpawnVehiclesSystem system;

        [SetUp]
        public void Setup()
        {
            world = new World();
            system = new SpawnVehiclesSystem(world);
        }

        [Test]
        public void Test_NoVehiclesSpawned_IfMaxCountReached()
        {
            for (var i = 0; i < Data.MaxVehicleCount; i++)
            {
                Utils.SpawnVehicles(world, 1, i % Data.MaxTeamCount);
            }

            system.Update(TestData.DeltaTime);

            var aliveCount = world.GetEntities().With<TeamDC>().AsSet().GetEntities().Length;
            Assert.AreEqual(Data.MaxVehicleCount, aliveCount);
        }

        [Test]
        public void Test_VehiclesSpawned_UntilMaxCountReached()
        {
            var initialCount = Data.MaxVehicleCount - 2;
            for (var i = 0; i < initialCount; i++)
            {
                Utils.SpawnVehicles(world, 1, i % Data.MaxTeamCount);
            }

            system.Update(TestData.DeltaTime);

            var aliveCount = world.GetEntities().With<TeamDC>().AsSet().GetEntities().Length;
            Assert.AreEqual(Data.MaxVehicleCount, aliveCount);
        }

        [TearDown]
        public void TearDown()
        {
            system.Dispose();
            world.Dispose();
        }
    }
}