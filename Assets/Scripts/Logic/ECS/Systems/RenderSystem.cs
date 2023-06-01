// Copyright (c) Sean Nowotny

using Logic.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Logic.ECS.Systems
{
    [UpdateInGroup(typeof(MySystemGroup), OrderLast = true)]
    public partial class RenderSystem : SystemBase
    {
        private static Transform[] transformPool = new Transform[DefaultECS.Data.MaxVehicleCount];
        private static MeshRenderer[] meshPool = new MeshRenderer[DefaultECS.Data.MaxVehicleCount];
        private bool initialized;

        protected override void OnUpdate()
        {
            if (!initialized && ManagedDataProvider.Instance.Prefab != null)
            {
                for (var i = 0; i < transformPool.Length; i++)
                {
                    transformPool[i] = GameObject.Instantiate(ManagedDataProvider.Instance.Prefab).transform;
                    meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
                }

                initialized = true;
            }

            using var entities = EntityManager.CreateEntityQuery(typeof(PositionDC)).ToEntityArray(Allocator.Temp);
            var entitiesCount = entities.Length;
            for (var i = 0; i < entitiesCount; i++)
            {
                var position = EntityManager.GetComponentData<PositionDC>(entities[i]).Value;
                transformPool[i].position = new Vector3(position.x, 0, position.y);
                
                int team = EntityManager.GetComponentData<TeamDC>(entities[i]).Value;
                meshPool[i].material = ManagedDataProvider.Instance.Materials[team];
                
                meshPool[i].enabled = true;
            }

            for (var i = entitiesCount; i < DefaultECS.Data.MaxVehicleCount; i++)
            {
                meshPool[i].enabled = false;
            }
        }
    }
}