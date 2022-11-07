using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Data.Entities;
using Training.Shared;

namespace Training.Data.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(AppConst.DbSchemas.Cms.Tables.Categories, AppConst.DbSchemas.Cms.Name);

            builder.HasKey(x => x.Id);

            builder.Property(p => p.RowVersion).IsRowVersion();
        }
    }
}