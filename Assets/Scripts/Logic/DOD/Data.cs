using Unity.Mathematics;

namespace Logic.DOD
{
    public static class Data
    {
        // Arrays should be sorted like this: Vehicles -> Dead
    
        public static bool EnableRendering = true;
        
        public static readonly int MaxVehicleCount = 1000;
        public static float2[] VehiclePositions = new float2[MaxVehicleCount];
        public static float[] VehicleHealths = new float[MaxVehicleCount];
        public static int[] VehicleTeams = new int[MaxVehicleCount];
        public static int[] VehicleTargets = new int[MaxVehicleCount];
        // public static bool[] VehicleAliveStatuses = new bool[MaxVehicleCount];
        public static readonly int MaxTeamCount = 4;
        public static int[] TeamAliveCounts = new int[MaxTeamCount];
        public static int AliveCount = 0;
        public static readonly float WeaponRange = 5;
        public static readonly float WeaponDamage = 100;
    }
}
