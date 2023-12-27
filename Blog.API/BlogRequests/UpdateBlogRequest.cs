namespace Blog.API.BlogRequests
{
    public class UpdateBlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
