using Blog.App.Db.Entities;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Db
{
    public class BlogDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPostEntity> Blogs { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserEntity>().ToTable("Users").HasKey(x => x.Id);
            builder.Entity<RoleEntity>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            builder.Entity<BlogPostEntity>().HasKey(x => x.PostId);
            builder.Entity<CategoryEntity>().HasKey(x => x.CategoryId);
            builder.Entity<AuthorEntity>().HasKey(x => x.AuthorId);

            builder.Entity<BlogPostEntity>()
            .HasOne(b => b.AuthorInformation)
            .WithMany(a => a.Blogs)
            .HasForeignKey(b => b.AuthorId);

            builder.Entity<RoleEntity>().HasData(new[]
            {
                new RoleEntity { Id = 1, Name = "user", NormalizedName = "USER" },
                new RoleEntity { Id = 2, Name = "admin", NormalizedName = "ADMIN" }
            });

            var userName = "admin@admin.com";
            var password = "blogadmin123";
            var birthday = "1990, 8, 11";

            var operatorUser = new UserEntity
            {
                Id = 1,
                Email = userName,
                UserName = userName,
                NormalizedEmail = userName.ToUpper(),
                NormalizedUserName = userName.ToUpper(),
                BirthDate = birthday
            };

            var hasher = new PasswordHasher<UserEntity>();
            operatorUser.PasswordHash = hasher.HashPassword(operatorUser, password);
            builder.Entity<UserEntity>().HasData(operatorUser);

            builder.Entity<IdentityUserRole<int>>().HasData(new[]
            {
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 }
            });
        }
    }
}
