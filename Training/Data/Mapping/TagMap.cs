using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Data.Entities;
using Training.Shared;

namespace Training.Data.Mapping
{
    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(AppConst.DbSchemas.Cms.Tables.Tags, AppConst.DbSchemas.Cms.Name);

            builder.HasKey(x => x.Id);
        }
    }
}