namespace BusinessLayer.Dtos.Blogs
{
    public class AddBlogDto
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public List<string> ImageList { get; set; }
        public int CustomerId { get; set; }
    }
}
