// Copyright (c) Sean Nowotny

using System;
using UnityEngine;

namespace Logic.BurstedDOD
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Material[] materials;

        private Data data;

        private void Start()
        {
            data = new Data(true);
        }

        private void Update()
        {
            StaticGameHandler.BurstedUpdate(Time.deltaTime, ref data);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                data.EnableRendering = !data.EnableRendering;
            }

            if (data.EnableRendering)
            {
                RenderSystem.Run(prefab, materials, ref data);
            }
        }

        private void OnDestroy()
        {
            data.Dispose();
        }
    }
}