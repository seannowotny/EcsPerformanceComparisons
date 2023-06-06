using Logic.BurstedAosDOD;
using NUnit.Framework;
using Unity.Mathematics;

namespace Pisces.Tests.BurstedAosDOD
{
    public class DieSystemTests
    {
        private Data data;
        private Random random;

        [SetUp]
        public void Setup()
        {
            data = new Data(false);
            random = new Random(42);
        }

        [Test]
        public void Test_VehicleDies_WhenHealthIsZero()
        {
            // Arrange
            Utils.SpawnVehicles(1, 0, ref data, ref random);
            var vehicle = new Vehicle { Health = 0, IsAlive = true };
            data.Vehicles[0] = vehicle;

            // Act
            DieSystem.Run(ref data);

            // Assert
            Assert.IsFalse(data.Vehicles[0].IsAlive);
        }

        [Test]
        public void Test_VehicleStaysAlive_WhenHealthIsAboveZero()
        {
            // Arrange
            Utils.SpawnVehicles(1, 0, ref data, ref random);
            var vehicle = new Vehicle { Health = 100, IsAlive = true };
            data.Vehicles[0] = vehicle;

            // Act
            DieSystem.Run(ref data);

            // Assert
            Assert.IsTrue(data.Vehicles[0].IsAlive);
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}