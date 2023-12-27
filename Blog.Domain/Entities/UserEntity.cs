using Microsoft.AspNetCore.Identity;

namespace Blog.App.Db.Entities
{
    public class UserEntity: IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BirthDate { get; set; }
    }
}
