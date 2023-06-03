// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.BurstedAosDOD
{
    public static class RenderSystem
    {
        private static Transform[] transformPool = new Transform[Data.MaxVehicleCount];
        private static MeshRenderer[] meshPool = new MeshRenderer[Data.MaxVehicleCount];

        public static void Run(GameObject prefab, Material[] materials, ref Data data)
        {
            if (meshPool[0] == null)
            {
                for (var i = 0; i < transformPool.Length; i++)
                {
                    transformPool[i] = GameObject.Instantiate(prefab).transform;
                    meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
                }
            }

            for (var i = 0; i < data.Vehicles.Length; i++)
            {
                if (!data.Vehicles[i].IsAlive)
                {
                    meshPool[i].enabled = false;
                    continue;
                }

                var position = data.Vehicles[i].Position;
                transformPool[i].position = new Vector3((float)position.x, 0, (float)position.y);
                meshPool[i].enabled = true;
                meshPool[i].material = materials[data.Vehicles[i].Team];
            }
        }
    }
}