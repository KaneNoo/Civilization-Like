namespace Shared.Models.Civilization
{
    public class Era
    {
        public Era()
        {

        }

        public Era(string name, int culturePointsRequared, int level)
        {

            Name = name;
            CulturePointsRequared = culturePointsRequared;
            Level = level;
        }
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CulturePointsRequared { get; set; }

        [Required]
        public int Level { get; set; }

        [JsonIgnore]
        public List<District> Districts { get; set; }

        public override string ToString()
        {
            return
                $"Наименование: {Name}\n" +
                $"Уровень: {Level}\n" +
                $"Пик Очков Культуры: {CulturePointsRequared}";
        }
    }
}
