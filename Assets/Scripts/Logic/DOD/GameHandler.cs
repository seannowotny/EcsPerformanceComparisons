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
            SpawnVehiclesSystem.Run();
            EnemyTargetSystem.Run();
            VehicleMovementSystem.Run((float) deltaTime);
            ShootSystem.Run((float) deltaTime);
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