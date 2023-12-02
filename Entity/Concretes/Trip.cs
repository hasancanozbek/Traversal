using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Trip : IEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public int GuideId { get; set; }
        public int Day { get; set; }
        public List<string> ImageList { get; set; }

        //Navigation Properties
        public virtual Guide Guide { get; set; }
        public virtual List<TripComment> TripComments { get; set; }
        public virtual List<TripDate> TripDates { get; set;}
        public virtual List<TripKey> TripKeys { get; set;}
    }
}
