using Blog.App.Db.Entities;
using System.Text.Json.Serialization;

namespace Blog.App.Models
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
        public AuthorEntity AuthorInformation { get; set; }
    }
}
