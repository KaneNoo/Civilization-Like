namespace Shared.Models.Civilization
{
    public class Mission
    {
        public Mission()
        {

        }

        public Mission(string title, string realDescription, string metaDescription, int culturePointsReward, int typeID)
        {
            Title = title;
            RealDescription = realDescription;
            MetaDescription = metaDescription;
            CulturePointsReward = culturePointsReward;
            TypeID = typeID;
        }

        public int ID { get; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string RealDescription { get; set; }

        [Required]
        public string MetaDescription { get; set; }
        
        [Required]
        public int CulturePointsReward { get; set; }

        [JsonIgnore]
        public MissionType Type { get; set; }

        [Required]
        public int TypeID { get; set; }


        [JsonIgnore]
        public List<PlayerMission> Players { get; set; } = new ();

        public override string ToString()
        {
            return
                $"{Title}\n" +
                $"Реальное Описание: {RealDescription}\n" +
                $"МетаОписание: {MetaDescription}\n" +
                $"Награда: {CulturePointsReward}";
        }
    }
}
