// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.DOD
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

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
                RenderSystem.Run(prefab);
            }
        }
    }
}