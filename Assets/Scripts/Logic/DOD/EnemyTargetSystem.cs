// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.DOD
{
    public class EnemyTargetSystem
    {
        public static void Run()
        {
            bool moreThanOneTeamAlive = false;

            {
                int aliveTeams = 0;
                for (var k = 0; k < Data.TeamAliveCounts.Length; k++)
                {
                    if (Data.TeamAliveCounts[k] == 0)
                    {
                        continue;
                    }

                    aliveTeams++;
                    if (aliveTeams > 1)
                    {
                        moreThanOneTeamAlive = true;
                        break;
                    }
                }
            }

            if (!moreThanOneTeamAlive)
            {
                return;
            }

            for (var i = 0; i < Data.AliveCount; i++)
            {
                if (!Data.VehicleAliveStatuses[i])
                {
                    continue;
                }

                var currentTargetIndex = Data.VehicleTargets[i];
                if (currentTargetIndex == -1 || !Data.VehicleAliveStatuses[currentTargetIndex])
                {
                    var currentTeam = Data.VehicleTeams[i];
                    int targetTeam;
                    int targetIndex;
                    do
                    {
                        targetIndex = Random.Range(0, Data.AliveCount); // Not deterministic
                        targetTeam = Data.VehicleTeams[targetIndex];
                    } while (targetTeam == currentTeam || !Data.VehicleAliveStatuses[targetIndex]);

                    Data.VehicleTargets[i] = targetIndex;
                }
            }
        }
    }
}