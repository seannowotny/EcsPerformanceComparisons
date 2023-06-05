// Copyright (c) Sean Nowotny

using Unity.Burst;
using UnityEngine;

namespace Logic.BurstedAosDODJob
{
    [BurstCompile]
    public static class EnemyTargetSystem
    {
        [BurstCompile]
        public static void Run(ref Unity.Mathematics.Random random, ref Data data)
        {
            bool moreThanOneTeamAlive = false;

            {
                int aliveTeams = 0;
                for (var k = 0; k < data.TeamAliveCounts.Length; k++)
                {
                    if (data.TeamAliveCounts[k] == 0)
                    {
                        continue;
                    }

                    aliveTeams++;
                    if (aliveTeams > 1)
                    {
                        moreThanOneTeamAlive = true;
                        break;
                    }
                }
            }

            if (!moreThanOneTeamAlive)
            {
                for (var i = 0; i < data.AliveCount; i++)
                {
                    data.Vehicles[i] = new Vehicle(data.Vehicles[i]) {TargetIndex = -1};
                }

                return;
            }

            for (var i = 0; i < data.AliveCount; i++)
            {
                var currentTargetIndex = data.Vehicles[i].TargetIndex;
                if (currentTargetIndex != -1 && data.Vehicles[currentTargetIndex].IsAlive)
                {
                    continue;
                }

                var currentTeam = data.Vehicles[i].Team;
                int enemyTeamIndex = 0;
                // do
                // {
                enemyTeamIndex = random.NextInt(0, data.MaxTeamCount);
                // } while (
                //     enemyTeamIndex == currentTeam ||
                //     (*Utils.TeamAliveNativeListFromIndex(enemyTeamIndex, ref data)).Length == 0
                // );

                int targetIndex = 0;
                switch (enemyTeamIndex)
                {
                    case 0:
                    {
                        targetIndex = data.Team0AliveVehicles[random.NextInt(0, data.Team0AliveVehicles.Length)];
                        break;
                    }
                    case 1:
                    {
                        targetIndex = data.Team1AliveVehicles[random.NextInt(0, data.Team1AliveVehicles.Length)];
                        break;
                    }
                    case 2:
                    {
                        targetIndex = data.Team2AliveVehicles[random.NextInt(0, data.Team2AliveVehicles.Length)];
                        break;
                    }
                    case 3:
                    {
                        targetIndex = data.Team3AliveVehicles[random.NextInt(0, data.Team3AliveVehicles.Length)];
                        break;
                    }
                    default:
                    {
                        Debug.LogError("Invalid Index");
                        break;
                    }
                }

                data.Vehicles[i] = new Vehicle(data.Vehicles[i]) {TargetIndex = targetIndex};
            }
        }
    }
}