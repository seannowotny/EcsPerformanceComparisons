// Copyright (c) Sean Nowotny

using DefaultEcs;
using DefaultEcs.System;
using Logic.DefaultECS;
using UnityEngine;

namespace Logic.DefaultECS
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Material[] materials;

        private World world;
        private SequentialSystem<float> sequentialSystem;
        private RenderSystem renderSystem;

        private void Start()
        {
            world = new World();

            sequentialSystem = new SequentialSystem<float>(
                new SpawnVehiclesSystem(world),
                new EnemyTargetSystem(world),
                new VehicleMovementSystem(world),
                new ShootSystem(world),
                new DieSystem(world)
            );

            renderSystem = new RenderSystem(world, prefab, materials);
            world.Subscribe(this);
        }

        private void Update()
        {
            sequentialSystem.Update(Time.deltaTime); // Not deterministic

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Data.EnableRendering = !Data.EnableRendering;
            }

            if (Data.EnableRendering)
            {
                renderSystem.Update(Time.deltaTime); // Not deterministic
            }
        }
    }
}