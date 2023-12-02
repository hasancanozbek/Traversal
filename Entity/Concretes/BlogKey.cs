using EntityLayer.Abstracts;

namespace EntityLayer.Concretes
{
    public class BlogKey : IEntity
    {
        public int BlogId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        //Navigation properties
        public Blog Blog { get; set; }
    }
}
