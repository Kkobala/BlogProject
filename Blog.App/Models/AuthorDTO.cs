using Blog.App.Db.Entities;

namespace Blog.App.Models
{
    public class AuthorDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BIO { get; set; }
        public string BirthDate { get; set; }
    }
}
