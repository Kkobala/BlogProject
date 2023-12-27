using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Infrastructure.Db
{
    public class BlogDbContextFactory: IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseSqlServer(args[0]);

            return new BlogDbContext(optionsBuilder.Options);
        }
    }
}
