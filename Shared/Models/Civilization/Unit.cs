using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Civilization
{
    public class Unit
    {

        public Unit()
        {

        }

        public Unit(string name)
        {
            Name = name;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<District> Districts { get; set; } = new List<District>();
    }
}
