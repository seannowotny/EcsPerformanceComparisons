using Unity.Burst;
using Unity.Collections;

namespace Logic.BurstedAosDOD
{
    [BurstCompile]
    public static class DieSystem
    {
        [BurstCompile]
        public static void Run(ref Data data)
        {
            for (int i = 0; i < data.Vehicles.Length; i++)
            {
                var vehicle = data.Vehicles[i];
                if (!vehicle.IsAlive)
                {
                    continue;
                }

                if (vehicle.Health > 0)
                {
                    continue;
                }

                Utils.DisposeVehicle(ref data, i);
            }
        }
    }
}