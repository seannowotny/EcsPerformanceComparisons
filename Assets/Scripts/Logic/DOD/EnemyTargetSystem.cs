// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.DOD
{
    public class EnemyTargetSystem
    {
        private static bool[] otherTeamAliveArr = new bool[Data.MaxTeamCount];

        public static void Run()
        {
            for (var i = 0; i < Data.MaxTeamCount; i++)
            {
                bool otherTeamAlive = false;
                for (var k = 0; k < Data.TeamAliveCounts.Length; k++)
                {
                    if (k == i)
                    {
                        continue;
                    }

                    if (Data.TeamAliveCounts[k] > 0)
                    {
                        otherTeamAlive = true;
                        break;
                    }
                }

                otherTeamAliveArr[i] = otherTeamAlive;
            }

            for (var i = 0; i < Data.AliveCount; i++)
            {
                if (!otherTeamAliveArr[Data.VehicleTeams[i]])
                {
                    continue;
                }

                if (Data.VehicleTargets[i] == -1 || Data.VehicleTargets[i] > Data.AliveCount - 1)
                {
                    var currentTeam = Data.VehicleTeams[i];
                    int targetTeam;
                    int targetIndex;
                    do
                    {
                        targetIndex = Random.Range(0, Data.AliveCount); // Not deterministic
                        targetTeam = Data.VehicleTeams[targetIndex];
                    } while (targetTeam == currentTeam);

                    Data.VehicleTargets[i] = targetIndex;
                }
            }
        }
    }
}