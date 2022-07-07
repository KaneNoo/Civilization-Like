using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models.FortuneWheel
{
    public class PlayerPrize
    {

        public PlayerPrize()
        {

        }

        public PlayerPrize(Player player, Prize prize)
        {
            Player = player;
            PlayerID = player.ID;
            Prize = prize;
            PrizeID = prize.ID;
        }
        public int ID { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }
        [Required]
        public int PlayerID { get; set; }

        [JsonIgnore]
        public Prize Prize { get; set; }
        [Required]
        public int PrizeID { get; set; }
    }
}
