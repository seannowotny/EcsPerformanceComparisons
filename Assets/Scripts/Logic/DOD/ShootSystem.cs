// Copyright (c) Sean Nowotny

using Unity.Mathematics;

namespace Logic.DOD
{
    public class ShootSystem
    {
        public static void Run(float deltaTime)
        {
            for (var i = 0; i < Data.AliveCount; i++)
            {
                var currentPosition = Data.VehiclePositions[i];
                var currentTarget = Data.VehicleTargets[i];
                var targetPosition = Data.VehiclePositions[currentTarget];
                if(math.distance(currentPosition, targetPosition) <= Data.WeaponRange)
                {
                    Data.VehicleHealths[currentTarget] -= Data.WeaponDamage * deltaTime;
                }
            }
        }
    }
}