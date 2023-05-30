// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.DOD
{
    public static class RenderSystem
    {
        private static Transform[] transformPool = new Transform[Data.MaxVehicleCount];
        private static MeshRenderer[] meshPool = new MeshRenderer[Data.MaxVehicleCount];

        public static void Run(GameObject prefab)
        {
            if (meshPool[0] == null)
            {
                for (var i = 0; i < transformPool.Length; i++)
                {
                    transformPool[i] = GameObject.Instantiate(prefab).transform;
                    meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
                }
            }

            for (var i = 0; i < Data.AliveCount; i++)
            {
                var position = Data.VehiclePositions[i];
                transformPool[i].position = new Vector3((float)position.x, 0, (float)position.y);
                meshPool[i].enabled = true;
            }

            for (var i = Data.AliveCount; i < Data.MaxVehicleCount; i++)
            {
                meshPool[i].enabled = false;
            }
        }
    }
}