using BusinessLayer.Dtos.Blogs;

namespace Traversal.Web.Areas.Admin.Models
{
    public class AdminUpdateBlogModel
    {
        public BlogDto CurrentBlog { get; set; }
        public UpdateBlogDto UpdateBlog { get; set; }
    }
}
