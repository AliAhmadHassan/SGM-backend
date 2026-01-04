using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class STMEmailMapping : IEntityTypeConfiguration<STMEmail>
    {
        public void Configure(EntityTypeBuilder<STMEmail> builder)
        {
            builder.ToTable("STM_EMAIL");
            builder.Property(x => x.Id).HasColumnName("STE_ID");
            builder.Property(x => x.STMId).HasColumnName("STM_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID");

            builder.HasOne(x => x.STM).WithMany(x => x.Emails).HasForeignKey(x => x.STMId);
            builder.HasOne(x => x.User).WithMany();
        }
    }
}