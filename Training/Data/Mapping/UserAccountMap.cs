using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Data.Entities;
using Training.Shared;

namespace Training.Data.Mapping
{
    public class UserAccountMap : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable(AppConst.DbSchemas.Cms.Tables.UserAccounts, AppConst.DbSchemas.Cms.Name);

            builder.HasKey(x => x.Id);

            builder.Property(p => p.RowVersion).IsRowVersion();
        }
    }
}