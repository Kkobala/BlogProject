namespace Blog.Domain.Entities
{
    public class BlogPostEntity
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public AuthorEntity AuthorInformation { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Quotes { get; set; }
        public string Conclusion { get; set; }
    }
}
