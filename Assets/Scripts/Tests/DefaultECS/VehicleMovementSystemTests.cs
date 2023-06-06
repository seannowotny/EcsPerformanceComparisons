using DefaultEcs;
using Logic.DefaultECS;
using Logic.DefaultECS.Components;
using NUnit.Framework;
using Unity.Mathematics;

namespace Pisces.Tests.DefaultECS
{
    public class VehicleMovementSystemTests
    {
        private World world;
        private VehicleMovementSystem system;

        [SetUp]
        public void Setup()
        {
            world = new World();
            system = new VehicleMovementSystem(world);
        }

        [Test]
        public void Test_NoMovement_IfTargetInRange()
        {
            var vehicle = world.CreateEntity();
            vehicle.Set(new PositionDC { Value = new float2(0, 0) });

            var target = world.CreateEntity();
            target.Set(new PositionDC { Value = new float2(Data.WeaponRange - 1, 0) });

            vehicle.Set(new TargetDC { Value = target });

            system.Update(TestData.DeltaTime);

            var newPosition = vehicle.Get<PositionDC>().Value;
            Assert.AreEqual(new float2(0, 0), newPosition);
        }

        [Test]
        public void Test_Movement_IfTargetOutOfRange()
        {
            var vehicle = world.CreateEntity();
            vehicle.Set(new PositionDC { Value = new float2(0, 0) });

            var target = world.CreateEntity();
            target.Set(new PositionDC { Value = new float2(Data.WeaponRange + 1, 0) });

            vehicle.Set(new TargetDC { Value = target });

            system.Update(TestData.DeltaTime);

            var newPosition = vehicle.Get<PositionDC>().Value;
            Assert.AreNotEqual(new float2(0, 0), newPosition);
        }
        
        [Test]
        public void Test_MovesTowardTarget_IfTargetOutOfRange()
        {
            var vehicle = world.CreateEntity();
            vehicle.Set(new PositionDC { Value = new float2(0, 0) });

            var target = world.CreateEntity();
            target.Set(new PositionDC { Value = new float2(Data.WeaponRange + 1, 0) });

            vehicle.Set(new TargetDC { Value = target });

            system.Update(TestData.DeltaTime);

            var newPosition = vehicle.Get<PositionDC>().Value;

            var expectedDirection = math.normalize(new float2(Data.WeaponRange + 1, 0) - new float2(0, 0));
            var actualDirection = math.normalize(newPosition - new float2(0, 0));
    
            Assert.IsTrue(math.all(math.abs(expectedDirection - actualDirection) < new float2(TestData.ErrorTolerance, TestData.ErrorTolerance)));
        }

        [TearDown]
        public void TearDown()
        {
            system.Dispose();
            world.Dispose();
        }
    }
}