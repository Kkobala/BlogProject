using Blog.App.Db.Entities;

namespace Blog.App.BlogRequests
{
    public class UpdateBlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
