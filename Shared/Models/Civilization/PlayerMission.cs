namespace Shared.Models.Civilization
{
    public class PlayerMission
    {

        public PlayerMission()
        {

        }

        public PlayerMission(int playerID, int missionID, DateTime deadline)
        {
            PlayerID = playerID;
            MissionID = missionID;
            Deadline = deadline;
        }

        public PlayerMission(Player player, Mission mission, DateTime deadline)
        {
            Player = player;
            PlayerID = player.ID;
            Mission = mission;
            MissionID = mission.ID;
            Deadline = deadline;
        }

        public int ID { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }
        public int PlayerID { get; set; }

        [JsonIgnore]
        public Mission Mission { get; set; }
        public int MissionID { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; }

        public bool Confirmed { get; set; } = false;

    }
}
