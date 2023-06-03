using System.Collections.Generic;
using UnityEngine;

namespace Logic.UnityDOD
{
    public static class Data
    {
        public static bool EnableRendering = true;

        public static readonly int MaxVehicleCount = 1000;
        public static Vector2[] VehiclePositions = new Vector2[MaxVehicleCount];
        public static float[] VehicleHealths = new float[MaxVehicleCount];
        public static int[] VehicleTeams = new int[MaxVehicleCount];
        public static int[] VehicleTargets = new int[MaxVehicleCount];
        public static bool[] VehicleAliveStatuses = new bool[MaxVehicleCount];
        public static readonly int MaxTeamCount = 4;
        
        public static int[] TeamAliveCounts = new int[MaxTeamCount];
        // public static HashSet<int>[] AliveTeamVehicleIndecies = new HashSet<int>[MaxTeamCount];
        public static List<int>[] TeamAliveVehicles = new List<int>[MaxTeamCount];
        
        public static int AliveCount = 0;
        public static readonly float WeaponRange = 5;
        public static readonly float WeaponDamage = 100;
        public static readonly float VehicleSpeed = 5;
    }
}