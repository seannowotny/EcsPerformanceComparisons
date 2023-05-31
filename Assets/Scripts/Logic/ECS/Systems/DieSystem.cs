// Copyright (c) Sean Nowotny

using Unity.Burst;
using Unity.Entities;

namespace Logic.ECS.Systems
{
    [UpdateInGroup(typeof(MySystemGroup))]
    [UpdateAfter(typeof(ShootSystem))]
    public partial struct DieSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI
                .GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (health, entity) in SystemAPI.Query<Components.HealthDC>().WithEntityAccess())
            {
                if (health.Value <= 0)
                {
                    ecb.DestroyEntity(entity);
                }
            }
        }
    }
}