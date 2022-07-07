using System.ComponentModel.DataAnnotations;

namespace Shared.Models.FortuneWheel
{
    public class Prize
    {

        public Prize()
        {

        }

        public Prize(string name, int chance, string color)
        {
            Name = name;
            Chance = chance;
            Color = color;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int Chance { get; set; }

        [JsonIgnore]
        public List<PlayerPrize> PlayerPrizes { get; set; } = new();

        [JsonIgnore]
        public List<Wheel> Wheels { get; set; } = new();


        public override string ToString()
        {
            return
                $"Наименование: {Name}\n" +
                $"Цвет фона: {Color}\n" +
                $"Шанс: {Chance}";
        }
    }
}
