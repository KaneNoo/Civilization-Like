
namespace Shared.Models
{
    public class Player
    {

        public Player (){
        }

        public Player (string name)
        {
            Name = name;
        }

        public int ID { get; }

        [Required]
        public string Name { get; set; }


        public bool IsAdmin { get; set; } = false;

        public int WheelCoins { get; set; } = 0;

        public District? District { get; set; }

        public int? DistrictID { get; set; }


        [JsonIgnore]
        public List<PlayerPrize>? PlayerPrizes { get; set; } = new();

        [JsonIgnore]
        public List<PlayerMission> Missions { get; set; } = new();


        public override string ToString()
        {
            return
                $"Имя: {Name}\n" +
                $"Закреплен за: {District.Name}\n" +
                $"Монеты Колеса Фортуны: { WheelCoins}\n";
        }
    }
}
