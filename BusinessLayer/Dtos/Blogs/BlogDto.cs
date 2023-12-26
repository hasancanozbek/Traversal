using BusinessLayer.Dtos.BlogComments;

namespace BusinessLayer.Dtos.Blogs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public List<string> ImageList { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<BlogCommentDto> Comments { get; set; }
    }
}
