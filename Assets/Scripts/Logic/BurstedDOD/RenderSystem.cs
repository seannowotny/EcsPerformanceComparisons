// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.BurstedDOD
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

            for (var i = 0; i < data.VehiclePositions.Length; i++)
            {
                if (!data.VehicleAliveStatuses[i])
                {
                    meshPool[i].enabled = false;
                    continue;
                }

                var position = data.VehiclePositions[i];
                transformPool[i].position = new Vector3((float)position.x, 0, (float)position.y);
                meshPool[i].enabled = true;
                meshPool[i].material = materials[data.VehicleTeams[i]];
            }
        }
    }
}