// Copyright (c) Sean Nowotny

using Logic.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;

namespace Logic.ECS.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct RandomSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            var randomEntity = state.EntityManager.CreateEntity();
            var random = new Random(42);
            state.EntityManager.AddComponentData(randomEntity, new SingletonRandom {Random = random});
        }

        public void OnUpdate(ref SystemState state)
        {
            SystemAPI.SetSingleton(
                new SingletonRandom
                {
                    Random = new Random(math.max((uint) (SystemAPI.Time.DeltaTime * 10000000), 33))
                }
            );
        }
    }
}