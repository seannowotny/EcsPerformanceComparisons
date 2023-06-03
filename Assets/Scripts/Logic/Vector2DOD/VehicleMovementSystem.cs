// Copyright (c) Sean Nowotny

using Unity.Mathematics;
using UnityEngine;

namespace Logic.UnityDOD
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

                var currentTarget = Data.VehicleTargets[i];
                if (currentTarget == -1)
                {
                    continue;
                }

                var currentPosition = Data.VehiclePositions[i];
                var targetPosition = Data.VehiclePositions[currentTarget];

                if (Vector2.Distance(currentPosition, targetPosition) < Data.WeaponRange)
                {
                    continue;
                }

                var direction = (targetPosition - currentPosition);
                direction.Normalize();
                
                var newPosition = currentPosition + direction * (Data.VehicleSpeed * deltaTime);
                Data.VehiclePositions[i] = newPosition;
            }
        }
    }
}