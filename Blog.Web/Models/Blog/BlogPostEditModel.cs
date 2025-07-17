using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Blog
{
    public class BlogPostEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
