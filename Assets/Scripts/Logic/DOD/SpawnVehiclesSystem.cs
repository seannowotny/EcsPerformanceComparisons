// Copyright (c) Sean Nowotny

namespace Logic.DOD
{
    public class SpawnVehiclesSystem
    {
        public static void Run()
        {
            if (Data.AliveCount < Data.MaxVehicleCount)
            {
                Utils.SpawnVehicles(1, 0);
                Utils.SpawnVehicles(1, 1);
            }
        }
    }
}