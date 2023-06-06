// Copyright (c) Sean Nowotny

using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Logic.BurstedAosDOD
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

            for (var i = 0; i < count; i++)
            {
                Vehicle vehicle = new Vehicle
                {
                    IsAlive = true,
                    Health = 100,
                    Position = new float2(random.NextFloat(0, 100), random.NextFloat(0, 100)),
                    TargetIndex = -1,
                    Team = teamIndex
                };

                CreateVehicle(ref data, vehicle);
            }
        }

        [BurstCompile]
        public static unsafe NativeList<int>* TeamAliveNativeListFromIndex(int index, ref Data data)
        {
            switch (index)
            {
                case 0:
                {
                    return (NativeList<int>*) UnsafeUtility.AddressOf(ref data.Team0AliveVehicles);
                }
                case 1:
                {
                    return (NativeList<int>*) UnsafeUtility.AddressOf(ref data.Team1AliveVehicles);
                }
                case 2:
                {
                    return (NativeList<int>*) UnsafeUtility.AddressOf(ref data.Team2AliveVehicles);
                }
                case 3:
                {
                    return (NativeList<int>*) UnsafeUtility.AddressOf(ref data.Team3AliveVehicles);
                }
                default:
                {
                    Debug.LogError("Invalid Index");
                    return (NativeList<int>*) UnsafeUtility.AddressOf(ref data.Team3AliveVehicles);
                }
            }
        }

        [BurstCompile]
        public static void DisposeVehicle(ref Data data, int index)
        {
            UpdateAliveDataForVehicle(ref data, index, false);
        }


        [BurstCompile]
        public static void CreateVehicle(ref Data data, in Vehicle vehicle)
        {
            if (data.AliveCount == Data.MaxVehicleCount)
            {
                return;
            }

            {
                bool isSpawnableIndex = false;
                for (var i = data.LastSpawnableVehicleIndex; i < data.Vehicles.Length; i++)
                {
                    if (data.Vehicles[i].IsAlive)
                    {
                        continue;
                    }

                    data.LastSpawnableVehicleIndex = i;
                    isSpawnableIndex = true;
                    break;
                }

                if (!isSpawnableIndex)
                {
                    for (var i = 0; i < data.Vehicles.Length; i++)
                    {
                        if (data.Vehicles[i].IsAlive)
                        {
                            continue;
                        }

                        data.LastSpawnableVehicleIndex = i;
                        break;
                    }
                }
            }

            UpdateAliveDataForVehicle(ref data, data.LastSpawnableVehicleIndex, true);
            data.Vehicles[data.LastSpawnableVehicleIndex] = vehicle;
        }

        [BurstCompile]
        private static unsafe void UpdateAliveDataForVehicle(ref Data data, int index, bool alive)
        {
            data.Vehicles[index] = new(data.Vehicles[index]) {IsAlive = alive};
            int team = data.Vehicles[index].Team;
            var val = alive ? 1 : -1;
            data.TeamAliveCounts[team] += val;
            data.AliveCount += val;

            var teamAliveList = *TeamAliveNativeListFromIndex(team, ref data);
            if (alive)
            {
                teamAliveList.Add(index);
            }
            else
            {
                teamAliveList.RemoveAt(teamAliveList.IndexOf(index));
            }
        }
    }
}