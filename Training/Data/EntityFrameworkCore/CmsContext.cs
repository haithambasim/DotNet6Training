using Microsoft.EntityFrameworkCore;
using Training.Data.Entities;
using Training.Data.Mapping;

namespace Training.Data.EntityFrameworkCore
{
    public class CmsContext : DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new TagMap());
        }
    }
}
