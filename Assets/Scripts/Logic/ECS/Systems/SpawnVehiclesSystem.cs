// Copyright (c) Sean Nowotny

using Logic.ECS.Components;
using Unity.Collections;
using Unity.Entities;

namespace Logic.ECS.Systems
{
    [UpdateBefore(typeof(DieSystem))]
    [UpdateBefore(typeof(ShootSystem))]
    [UpdateInGroup(typeof(MySystemGroup))]
    public partial struct SpawnVehiclesSystem : ISystem
    {
        private EntityQuery query;

        public void OnCreate(ref SystemState state)
        {
            var types = new NativeList<ComponentType>(Allocator.Temp);
            types.Add(ComponentType.ReadOnly<TeamDC>());
            query = state.GetEntityQuery(types);

            types.Dispose();
        }

        public void OnUpdate(ref SystemState state)
        {
            var randomSingleton = SystemAPI.GetSingleton<SingletonRandom>();
            var ecb = SystemAPI
                .GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            var aliveCount = query.CalculateEntityCount();
            int vacantSlots = Data.MaxVehicleCount - aliveCount;

            for (var i = 0; i < Data.MaxTeamCount; i++)
            {
                if (vacantSlots == 0)
                {
                    break;
                }

                var vehicle = ecb.CreateEntity();
                ecb.AddComponent(
                    vehicle,
                    new PositionDC
                    {
                        Value = new(randomSingleton.Random.NextFloat(0, 100), randomSingleton.Random.NextFloat(0, 100))
                    }
                );
                ecb.AddComponent(
                    vehicle,
                    new TeamDC
                    {
                        Value = i
                    }
                );
                ecb.AddComponent<TargetDC>(vehicle);
                ecb.AddComponent(vehicle,
                    new HealthDC
                    {
                        Value = 100
                    }
                );

                vacantSlots--;
            }
        }
    }
}