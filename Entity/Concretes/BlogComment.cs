using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class BlogComment : IEntity
    {
        public int CustomerId { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }

        //Navigaiton Properties
        public Customer Customer { get; set; }
        public Blog Blog { get; set; }
    }
}
