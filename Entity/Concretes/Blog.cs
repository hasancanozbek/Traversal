using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class Blog : IEntity
    {
        public string Content { get; set; }
        public string Title { get; set; }

        //Navigation Properties
        public virtual List<BlogComment> BlogComment { get; set; }
        public virtual List<BlogKey> BlogKeys { get; set; }
    }
}
