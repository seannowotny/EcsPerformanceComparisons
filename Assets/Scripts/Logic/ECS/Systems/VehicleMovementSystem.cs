// Copyright (c) Sean Nowotny

using Logic.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.ECS.Systems
{
    [UpdateInGroup(typeof(MySystemGroup))]
    [UpdateAfter(typeof(EnemyTargetSystem))]
    public partial struct VehicleMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float speed = 5;
            foreach (var (target, position)in SystemAPI.Query<TargetDC, RefRW<PositionDC>>())
            {
                var targetEntity = target.Value;
                if (targetEntity == default)
                {
                    continue;
                }

                var currentPosition = position.ValueRO.Value;
                var targetPosition = SystemAPI.GetComponent<PositionDC>(targetEntity).Value;
                
                if (math.distance(currentPosition, targetPosition) < DefaultECS.Data.WeaponRange)
                {
                    continue;
                }

                var direction = math.normalize(targetPosition - currentPosition);
                var newPosition = currentPosition + direction * speed * Time.captureDeltaTime;
                position.ValueRW = new PositionDC {Value = newPosition};
            }
        }
    }
}