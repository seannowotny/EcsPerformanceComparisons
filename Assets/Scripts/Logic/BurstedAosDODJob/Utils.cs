// Copyright (c) Sean Nowotny

using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Logic.BurstedAosDODJob
{
    [BurstCompile]
    public static class Utils
    {
        [BurstCompile]
        public static void SpawnVehicles(int count, int teamIndex, ref Data data, ref Random random)
        {
            if (count == 0)
            {
                return;
            }

            int spawned = 0;

            for (var i = 0; i < data.MaxVehicleCount; i++)
            {
                if (data.Vehicles[i].IsAlive)
                {
                    continue;
                }

                Vehicle vehicle = new Vehicle
                {
                    IsAlive = true,
                    Health = 100,
                    Position = new float2(random.NextFloat(0, 100), random.NextFloat(0, 100)),
                    TargetIndex = -1,
                    Team = teamIndex
                };

                data.Vehicles[i] = vehicle;
                data.TeamAliveCounts[teamIndex]++;

                switch (teamIndex)
                {
                    case 0:
                    {
                        data.Team0AliveVehicles.Add(i);
                        break;
                    }
                    case 1:
                    {
                        data.Team1AliveVehicles.Add(i);
                        break;
                    }
                    case 2:
                    {
                        data.Team2AliveVehicles.Add(i);
                        break;
                    }
                    case 3:
                    {
                        data.Team3AliveVehicles.Add(i);
                        break;
                    }
                    default:
                    {
                        Debug.LogError("Invalid Index");
                        break;
                    }
                }

                data.AliveCount++;

                spawned++;
                if (spawned == count)
                {
                    break;
                }
            }
        }
    }
}