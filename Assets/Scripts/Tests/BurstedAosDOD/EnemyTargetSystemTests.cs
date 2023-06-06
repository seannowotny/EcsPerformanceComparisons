using Logic.BurstedAosDOD;
using NUnit.Framework;
using Unity.Mathematics;

namespace Pisces.Tests.BurstedAosDOD
{
    public class EnemyTargetSystemTests
    {
        private Data data;
        private Random random;

        [SetUp]
        public void Setup()
        {
            data = new Data(false);
            random = new Random(1234);  // deterministic random for test repeatability
            
            // Spawn 10 vehicles for each team
            for (int i = 0; i < Data.MaxTeamCount; i++)
            {
                Utils.SpawnVehicles(10, i, ref data, ref random);
            }
        }

        [Test]
        public void Test_AllTargetsCleared_IfOneTeamAlive()
        {
            // Arrange
            for (int i = 0; i < Data.MaxTeamCount; i++)
            {
                if (i != 0) // Kill all vehicles except for team 0
                {
                    for (int j = 0; j < 10; j++)
                    {
                        int index = data.Team0AliveVehicles[j];
                        var vehicle = data.Vehicles[index];
                        vehicle.IsAlive = false;
                        vehicle.TargetIndex = -1;
                        data.Vehicles[index] = vehicle;
                    }
                }
            }

            // Act
            EnemyTargetSystem.Run(ref data);

            // Assert
            for (int i = 0; i < 10; i++)
            {
                int index = data.Team0AliveVehicles[i];
                var vehicle = data.Vehicles[index];
                Assert.AreEqual(-1, vehicle.TargetIndex);
            }
        }

        [Test]
        public void Test_TargetsAssigned_IfMultipleTeamsAlive()
        {
            // Arrange - already done in the Setup()

            // Act
            EnemyTargetSystem.Run(ref data);

            // Assert
            for (int i = 0; i < Data.MaxTeamCount; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int index = data.Team0AliveVehicles[j];
                    var vehicle = data.Vehicles[index];
                    Assert.AreNotEqual(-1, vehicle.TargetIndex);
                    Assert.AreNotEqual(vehicle.Team, data.Vehicles[vehicle.TargetIndex].Team);
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
