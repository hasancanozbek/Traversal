
namespace EntityLayer.Abstracts
{
    public class IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
