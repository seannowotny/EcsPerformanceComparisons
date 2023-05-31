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
    public partial struct ShootSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentPosition, target) in SystemAPI.Query<PositionDC, TargetDC>())
            {
                var targetPosition = SystemAPI.GetComponent<PositionDC>(target.Value);
                if (math.distance(currentPosition.Value, targetPosition.Value) <= Data.WeaponRange)
                {
                    var targetHealth = SystemAPI.GetComponent<HealthDC>(target.Value).Value;
                    SystemAPI.SetComponent(
                        target.Value,
                        new HealthDC
                        {
                            Value = targetHealth - Data.WeaponDamage * Time.captureDeltaTime
                        }
                    );
                }
            }
        }
    }
}