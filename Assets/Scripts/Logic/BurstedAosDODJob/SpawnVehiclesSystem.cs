// Copyright (c) Sean Nowotny

using Unity.Burst;
using Unity.Mathematics;

namespace Logic.BurstedAosDODJob
{
    [BurstCompile]
    public static class SpawnVehiclesSystem
    {
        [BurstCompile]
        public static void Run(ref Random random, ref Data data)
        {
            for (var i = 0; i < Data.MaxTeamCount; i++)
            {
                if (data.AliveCount == Data.MaxVehicleCount)
                {
                    break;
                }

                Utils.SpawnVehicles(1, i, ref data, ref random);
            }
        }
    }
}