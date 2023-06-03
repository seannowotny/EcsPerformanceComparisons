// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.aosDOD
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Material[] materials;

        private void Start()
        {
            for (var i = 0; i < Data.TeamAliveVehicles.Length; i++)
            {
                Data.TeamAliveVehicles[i] = new();
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime; // Not deterministic
            
            EnemyTargetSystem.Run();
            VehicleMovementSystem.Run(deltaTime);
            ShootSystem.Run(deltaTime);
            
            SpawnVehiclesSystem.Run();
            DieSystem.Run();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Data.EnableRendering = !Data.EnableRendering;
            }

            if (Data.EnableRendering)
            {
                RenderSystem.Run(prefab, materials);
            }
        }
    }
}