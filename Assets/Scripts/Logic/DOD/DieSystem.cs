namespace Logic.DOD
{
    public class DieSystem
    {
        public static void Run()
        {
            for (int i = 0; i < Data.VehicleHealths.Length; i++)
            {
                if (!Data.VehicleAliveStatuses[i])
                {
                    continue;
                }

                if (Data.VehicleHealths[i] > 0)
                {
                    continue;
                }

                Data.VehicleAliveStatuses[i] = false;
                Data.TeamAliveCounts[Data.VehicleTeams[i]]--;
                Data.AliveCount--;
            }
        }
    }
}