using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class TripComment : IEntity
    {
        public int CustomerId { get; set; }
        public int TripDateId { get; set; }
        public string Text { get; set; }
        public int Star { get; set; }

        //Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual TripDate TripDate { get; set; }
    }
}
