// Copyright (c) Sean Nowotny

using Unity.Burst;

namespace Logic.BurstedDOD
{
    [BurstCompile]
    public static class StaticGameHandler
    {
        [BurstCompile]
        public static void BurstedUpdate(float deltaTime, ref Data data)
        {
            EnemyTargetSystem.Run(ref data);
            VehicleMovementSystem.Run(deltaTime, ref data);
            ShootSystem.Run(deltaTime, ref data);
            
            SpawnVehiclesSystem.Run(ref data);
            DieSystem.Run(ref data);
        }
    }
}