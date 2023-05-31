// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.DOD
{
    public static class Utils
    {
        public static void SpawnVehicles(int count, int teamIndex)
        {
            int deadVehiclesStartIndex = Data.AliveCount;
            int deadVehiclesCount = Data.MaxVehicleCount - Data.AliveCount;
            if (deadVehiclesCount == 0)
            {
                return;
            }

            for (var i = deadVehiclesStartIndex; i < deadVehiclesStartIndex + count && i < Data.MaxVehicleCount; i++)
            {
                // Data.VehicleAliveStatuses[i] = true;
                Data.VehicleHealths[i] = 100;
                Data.VehiclePositions[i] = new(Random.Range(0, 100), Random.Range(0, 100));
                Data.VehicleTargets[i] = -1;
                Data.VehicleTeams[i] = teamIndex;
                Data.TeamAliveCounts[teamIndex]++;
                Data.AliveCount++;
            }
        }
    }
}