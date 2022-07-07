
namespace Shared.Models.Civilization
{
    public class MissionType
    {
        public MissionType()
        {

        }
        public MissionType(string name)
        {
            Name = name;
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
