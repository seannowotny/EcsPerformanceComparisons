namespace Logic.DOD
{
    public class DieSystem
    {
        public static void Run()
        {
            int end = Data.AliveCount - 1;

            for (int i = 0; i <= end; i++)
            {
                if (Data.VehicleHealths[i] <= 0)
                {
                    // Decrease team alive count
                    Data.TeamAliveCounts[Data.VehicleTeams[i]]--;
                    
                    // Overwrite data of dead vehicle with the last valid vehicle data
                    Data.VehiclePositions[i] = Data.VehiclePositions[end];
                    Data.VehicleHealths[i] = Data.VehicleHealths[end];
                    Data.VehicleTeams[i] = Data.VehicleTeams[end];
                    Data.VehicleTargets[i] = Data.VehicleTargets[end];

                    // Decrease the end index and adjust i to check the new vehicle data at this index
                    end--;
                    i--;
                }
            }

            // Update the AliveCount to the number of vehicles still alive
            Data.AliveCount = end + 1;
        }
    }
}