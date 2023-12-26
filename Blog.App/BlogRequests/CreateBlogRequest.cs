using Blog.App.Db.Entities;

namespace Blog.App.BlogRequests
{
    public class CreateBlogRequest
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Quotes { get; set; }
        public string Conclusion { get; set; }
    }
}
