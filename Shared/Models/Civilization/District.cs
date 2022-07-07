

namespace Shared.Models.Civilization
{

    public class District
    {
        public District()
        {

        }

        public District(string name, int unitID)
        {
            Name = name;
            UnitID = unitID;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        
        public int CulturePoints { get; set; } = 0;

        [JsonIgnore]
        public Unit Unit { get; set; }

        public int UnitID { get; set; }

        [JsonIgnore]
        public Era Era { get; set; }
        public int EraID { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        public override string ToString()
        {
            return
                $"Имя: {Name}\n" +
                $"Привязка: {Unit.Name}";
        }
    }
}
