using Unity.Entities;

namespace ECS
{
    public class CustomBootstrap : ICustomBootstrap
    {
        public bool Initialize(string defaultWorldName)
        {
            // Bootstrapping unused DefaultWorld
            var world = new World(defaultWorldName);
            World.DefaultGameObjectInjectionWorld = world;
            return true;
        }
    }
}