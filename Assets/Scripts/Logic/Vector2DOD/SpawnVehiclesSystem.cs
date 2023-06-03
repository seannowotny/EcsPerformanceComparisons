// Copyright (c) Sean Nowotny

namespace Logic.UnityDOD
{
    public class SpawnVehiclesSystem
    {
        public static void Run()
        {
            for (var i = 0; i < Data.MaxTeamCount; i++)
            {
                if (Data.AliveCount == Data.MaxVehicleCount)
                {
                    break;
                }

                Utils.SpawnVehicles(1, i);
            }
        }
    }
}