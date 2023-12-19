using Blog.App.Db.Entities;

namespace Blog.App.Models
{
    public class AuthorDTO
    {
        //public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BIO { get; set; }
        public string BirthDate { get; set; }


        //public List<BlogPostEntity> Blogs { get; set; }
    }
}
