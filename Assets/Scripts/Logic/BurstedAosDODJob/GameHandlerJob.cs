// Copyright (c) Sean Nowotny

using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Logic.BurstedAosDODJob
{
    [BurstCompile]
    public struct GameHandlerJob : IJob
    {
        [ReadOnly] public float DeltaTime;

        public NativeReference<int> AliveCount;
        public NativeArray<Vehicle> Vehicles;
        public NativeArray<int> TeamAliveCounts;
        public NativeList<int> Team0AliveVehicles;
        public NativeList<int> Team1AliveVehicles;
        public NativeList<int> Team2AliveVehicles;
        public NativeList<int> Team3AliveVehicles;

        [BurstCompile]
        public void Execute()
        {
            var data = Data.Create();
            ref var dataRef = ref data;
            dataRef.AliveCount = AliveCount.Value;
            dataRef.Vehicles = Vehicles;
            dataRef.TeamAliveCounts = TeamAliveCounts;
            dataRef.Team0AliveVehicles = Team0AliveVehicles;
            dataRef.Team1AliveVehicles = Team1AliveVehicles;
            dataRef.Team2AliveVehicles = Team2AliveVehicles;
            dataRef.Team3AliveVehicles = Team3AliveVehicles;

            Random random = new Random((uint) (DeltaTime * 10000000));

            EnemyTargetSystem.Run(ref random, ref data);
            VehicleMovementSystem.Run(DeltaTime, ref data);
            ShootSystem.Run(DeltaTime, ref data);

            SpawnVehiclesSystem.Run(ref random, ref data);
            DieSystem.Run(ref data);

            AliveCount.Value = data.AliveCount;
        }
    }
}