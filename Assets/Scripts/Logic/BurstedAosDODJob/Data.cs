using System;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace Logic.BurstedAosDODJob
{
    public struct Vehicle
    {
        public Vehicle(Vehicle vehicle)
        {
            Position = vehicle.Position;
            Health = vehicle.Health;
            Team = vehicle.Team;
            TargetIndex = vehicle.TargetIndex;
            IsAlive = vehicle.IsAlive;
        }

        public float2 Position;
        public float Health;
        public int Team;
        public int TargetIndex;
        public bool IsAlive;
    }

    [BurstCompile]
    public struct Data : IDisposable
    {
        public int MaxVehicleCount;
        public int MaxTeamCount;
        public float WeaponRange;
        public float WeaponDamage;
        public float VehicleSpeed;

        [MarshalAs(UnmanagedType.U1)] public bool EnableRendering;
        public int AliveCount;

        public NativeArray<Vehicle> Vehicles;

        public NativeArray<int> TeamAliveCounts;

        public NativeList<int> Team0AliveVehicles;
        public NativeList<int> Team1AliveVehicles;
        public NativeList<int> Team2AliveVehicles;
        public NativeList<int> Team3AliveVehicles;

        public static Data Create(bool enabledRendering = true)
        {
            return new Data
            {
                MaxVehicleCount = 1000,
                MaxTeamCount = 4,
                WeaponRange = 5,
                WeaponDamage = 100,
                VehicleSpeed = 5,
                EnableRendering = enabledRendering,
                AliveCount = 0,
            };
        }
        
        public static Data CreateWithContainers(bool enabledRendering = true)
        {
            int maxVehicleCount = 1000;
            int maxTeamCount = 4;
            return new Data
            {
                MaxVehicleCount = maxVehicleCount,
                MaxTeamCount = maxTeamCount,
                WeaponRange = 5,
                WeaponDamage = 100,
                VehicleSpeed = 5,
                EnableRendering = enabledRendering,
                AliveCount = 0,
                Vehicles = new NativeArray<Vehicle>(maxVehicleCount, Allocator.Persistent),
                TeamAliveCounts = new NativeArray<int>(maxTeamCount, Allocator.Persistent),
                Team0AliveVehicles = new NativeList<int>(maxVehicleCount, Allocator.Persistent),
                Team1AliveVehicles = new NativeList<int>(maxVehicleCount, Allocator.Persistent),
                Team2AliveVehicles = new NativeList<int>(maxVehicleCount, Allocator.Persistent),
                Team3AliveVehicles = new NativeList<int>(maxVehicleCount, Allocator.Persistent),
            };
        }

        public void Dispose()
        {
            Vehicles.Dispose();
            TeamAliveCounts.Dispose();
            Team0AliveVehicles.Dispose();
            Team1AliveVehicles.Dispose();
            Team2AliveVehicles.Dispose();
            Team3AliveVehicles.Dispose();
        }
    }
}