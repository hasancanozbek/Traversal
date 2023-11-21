namespace BusinessLayer.Dtos.BlogComments
{
    public class BlogCommentDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int CustomerId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
