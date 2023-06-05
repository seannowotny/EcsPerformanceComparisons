using Unity.Burst;
using UnityEngine;

namespace Logic.BurstedAosDODJob
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

                data.Vehicles[i] = new Vehicle(vehicle) {IsAlive = false};
                
                unsafe
                {
                    switch (vehicle.Team)
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
                }
                
                data.TeamAliveCounts[vehicle.Team]--;
                data.AliveCount--;
            }
        }
    }
}