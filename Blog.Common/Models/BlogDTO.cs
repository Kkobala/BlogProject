using System.Text.Json.Serialization;

namespace Blog.Common.Models
{
    public class BlogDTO
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Quotes { get; set; }
        public string Conclusion { get; set; }

        [JsonIgnore]
        public AuthorDTO AuthorInformation { get; set; }
    }
}
