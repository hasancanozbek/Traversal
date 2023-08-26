using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Blog : IEntity
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public List<string> ImageList { get; set; }
        public int AuthorId { get; set; }

        //Navigation Properties
        public Customer Customer { get; set; }
    }
}
