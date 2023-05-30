// Copyright (c) Sean Nowotny

using DefaultEcs;
using DefaultEcs.System;
using Logic.DefaultECS.Components;

namespace Logic.DefaultECS
{
    public class SpawnVehiclesSystem : ISystem<float>
    {
        private readonly World world;
        private readonly EntitySet alive;

        public SpawnVehiclesSystem(World _world)
        {
            world = _world;
            alive = world.GetEntities().With<TeamDC>().AsSet();
        }
        
        public bool IsEnabled { get; set; } = true;

        public void Update(float state)
        {
            var aliveCount = alive.GetEntities().Length;
            if (aliveCount < Data.MaxVehicleCount)
            {
                Utils.SpawnVehicles(world, 1, 0);
                Utils.SpawnVehicles(world, 1, 1);
            }
        }

        public void Dispose()
        {
        }
    }
}