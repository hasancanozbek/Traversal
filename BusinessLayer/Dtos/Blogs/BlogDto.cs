using BusinessLayer.Dtos.BlogComments;

namespace BusinessLayer.Dtos.Blogs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public List<string> ImageList { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<BlogCommentDto> Comments { get; set; }
        public bool IsActive { get; set; }
    }
}
