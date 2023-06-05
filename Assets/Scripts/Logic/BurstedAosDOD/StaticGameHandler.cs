// Copyright (c) Sean Nowotny

using Unity.Burst;

namespace Logic.BurstedAosDOD
{
    [BurstCompile]
    public static class StaticGameHandler
    {
        [BurstCompile]
        public static void BurstedUpdate(float deltaTime, ref Data data)
        {
            Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint) (deltaTime * 10000000));
            
            EnemyTargetSystem.Run(ref random, ref data);
            VehicleMovementSystem.Run(deltaTime, ref data);
            ShootSystem.Run(deltaTime, ref data);
            
            SpawnVehiclesSystem.Run(ref random, ref data);
            DieSystem.Run(ref data);
        }
    }
}