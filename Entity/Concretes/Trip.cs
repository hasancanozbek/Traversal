using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Trip : IEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public int Capacity { get; set; }
        public DateTime PlannedDate { get; set; }
        public int Day { get; set; }
        public List<string> ImageList { get; set; }

        //Navigation Properties
        public Guide Guide { get; set; }
        public List<Location> LocationList { get; set; }
    }
}
