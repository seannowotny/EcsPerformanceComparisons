// Copyright (c) Sean Nowotny

using Unity.Mathematics;

namespace Logic.DOD
{
    public class VehicleMovementSystem
    {
        public static void Run(float deltaTime)
        {
            for (var i = 0; i < Data.AliveCount; i++)
            {
                if (!Data.VehicleAliveStatuses[i])
                {
                    continue;
                }

                float speed = 5;
                var currentPosition = Data.VehiclePositions[i];
                var targetPosition = Data.VehiclePositions[Data.VehicleTargets[i]];
                
                if (math.distance(currentPosition, targetPosition) < Data.WeaponRange)
                {
                    continue;
                }
                
                var direction = math.normalize(targetPosition - currentPosition);
                var newPosition = currentPosition + direction * speed * deltaTime;
                Data.VehiclePositions[i] = newPosition;
            }
        }
    }
}