using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class STMMaterialMapping : IEntityTypeConfiguration<STMMaterial>
    {
        public void Configure(EntityTypeBuilder<STMMaterial> builder)
        {
            builder.ToTable("STM_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("STA_ID");
            builder.Property(x => x.Amount).HasColumnName("STA_QUANTIDADE");
            builder.Property(x => x.STMId).HasColumnName("STM_ID");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.Item).HasColumnName("STA_ITEM");

            builder.HasOne(x => x.STM).WithMany(x => x.STMMaterials).HasForeignKey(x => x.STMId);
            builder.HasOne(x => x.Material).WithMany(x => x.STMMaterials).HasForeignKey(x => x.MaterialId);
            builder.HasMany(x => x.TransferMaterials).WithOne(x => x.STMMaterial);
        }
    }
}