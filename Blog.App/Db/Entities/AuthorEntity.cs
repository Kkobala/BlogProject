using Blog.App.Models;

namespace Blog.App.Db.Entities
{
    public class AuthorEntity
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BIO { get; set; }
        public string BirthDate {  get; set; }


        public List<BlogPostEntity> Blogs { get; set; }


        public static implicit operator AuthorDTO(AuthorEntity author)
        {
            return new AuthorDTO
            {
                Name = author.Name,
                LastName = author.LastName,
                BIO = author.BIO,
                BirthDate = author.BirthDate,
            };
        }

        public AuthorEntity()
        {
            Blogs = new List<BlogPostEntity>();
        }
    }
}
