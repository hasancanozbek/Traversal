namespace BusinessLayer.Dtos.Blogs
{
    public class UpdateBlogDto
    {
        public string? Content { get; set; }
        public string? Title { get; set; }
        public List<string>? ImageList { get; set; }
    }
}
