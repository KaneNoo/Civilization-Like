namespace Shared.Models.FortuneWheel
{
    public class Wheel
    {
        public Wheel()
        {

        }

        public int ID { get; set; }

        public Prize Prize { get; set; }
        public int PrizeID { get; set; }
    }
}
