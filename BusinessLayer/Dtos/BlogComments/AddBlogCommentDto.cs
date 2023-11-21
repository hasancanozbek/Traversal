namespace BusinessLayer.Dtos.BlogComments
{
    public class AddBlogCommentDto
    {
        public int CustomerId { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }
    }
}
