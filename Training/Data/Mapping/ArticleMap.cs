using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Data.Entities;
using Training.Shared;

namespace Training.Data.Mapping
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(AppConst.DbSchemas.Cms.Tables.Articles, AppConst.DbSchemas.Cms.Name);

            builder.HasKey(x => x.Id);

            builder.HasMany(p => p.Tags)
                    .WithMany(p => p.Articles)
                    .UsingEntity(j => j.ToTable("ArticleTags"));

            builder.Property(p => p.RowVersion).IsRowVersion();
        }
    }
}