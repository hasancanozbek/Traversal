using BusinessLayer.Dtos.BlogComments;
using BusinessLayer.Dtos.Blogs;

namespace Traversal.Web.Models
{
    public class BlogDetailViewModel
    {
        public BlogDto Blog { get; set; }
        public AddBlogCommentDto AddBlogCommentDto { get; set; }
    }
}
