// Copyright (c) Sean Nowotny

using DefaultEcs;
using DefaultEcs.System;
using Logic.DefaultECS.Components;
using UnityEngine;

namespace Logic.DefaultECS
{
    public class RenderSystem : ISystem<float>
    {
        private static Transform[] transformPool = new Transform[Data.MaxVehicleCount];
        private static MeshRenderer[] meshPool = new MeshRenderer[Data.MaxVehicleCount];
        private readonly GameObject prefab;
        private readonly World world;
        private readonly EntitySet entities;

        public bool IsEnabled { get; set; } = true;

        public RenderSystem(World _world, GameObject _prefab)
        {
            world = _world;
            prefab = _prefab;

            entities = world.GetEntities().With<PositionDC>().AsSet();
            
            for (var i = 0; i < transformPool.Length; i++)
            {
                transformPool[i] = GameObject.Instantiate(prefab).transform;
                meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
            }
        }

        public void Update(float state)
        {
            var nonce = entities.GetEntities();
            var entitiesCount = nonce.Length;
            for (var i = 0; i < entitiesCount; i++)
            {
                var position = nonce[i].Get<PositionDC>().Value;
                transformPool[i].position = new Vector3((float) position.x, 0, (float) position.y);
                meshPool[i].enabled = true;
            }

            for (var i = entitiesCount; i < Data.MaxVehicleCount; i++)
            {
                meshPool[i].enabled = false;
            }
        }

        public void Dispose()
        {
        }
    }
}