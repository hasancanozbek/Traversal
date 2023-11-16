using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Comment : IEntity
    {
        public int CustomerId { get; set; }
        public int TripId { get; set; }
        public string Text { get; set; }
        public int Star { get; set; }

        //Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
