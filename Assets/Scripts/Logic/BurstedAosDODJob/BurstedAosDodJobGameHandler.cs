// Copyright (c) Sean Nowotny

using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Logic.BurstedAosDODJob
{
    public class BurstedAosDodJobGameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Material[] materials;

        private Data data;
        private GameHandlerJob job;
        private bool initialized;
        private JobHandle jobHandle;

        private void Start()
        {
            data = Data.CreateWithContainers();
            RenderSystem.Initialize(prefab, materials);
        }

        private void Update()
        {
            ref var dataRef = ref data;

            if (initialized)
            {
                jobHandle.Complete();
                dataRef.AliveCount = job.AliveCount.Value;
            }

            initialized = true;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                dataRef.EnableRendering = !data.EnableRendering;

                if (dataRef.EnableRendering)
                {
                    RenderSystem.Initialize(prefab, materials);
                }
                else
                {
                    RenderSystem.Clear();
                }
            }

            if (data.EnableRendering)
            {
                RenderSystem.Run(ref dataRef);
            }

            var aliveCountNativeRef = new NativeReference<int>(Allocator.TempJob);
            aliveCountNativeRef.Value = dataRef.AliveCount;
            job = new GameHandlerJob
            {
                DeltaTime = Time.deltaTime,
                AliveCount = aliveCountNativeRef,
                Vehicles = dataRef.Vehicles,
                TeamAliveCounts = dataRef.TeamAliveCounts,
                Team0AliveVehicles = dataRef.Team0AliveVehicles,
                Team1AliveVehicles = dataRef.Team1AliveVehicles,
                Team2AliveVehicles = dataRef.Team2AliveVehicles,
                Team3AliveVehicles = dataRef.Team3AliveVehicles
            };
            jobHandle = job.Schedule();
        }

        private void OnDestroy()
        {
            data.Dispose();
        }
    }
}