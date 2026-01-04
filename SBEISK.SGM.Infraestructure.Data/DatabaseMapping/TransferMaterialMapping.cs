using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferMaterialMapping : IEntityTypeConfiguration<TransferMaterial>
    {
        public void Configure(EntityTypeBuilder<TransferMaterial> builder)
        {
            builder.ToTable("TRANSFERENCIA_MATERIAL");
            builder.Property(t => t.Id).HasColumnName("TRM_ID");
            builder.Property(t => t.Amount).HasColumnName("TRM_QUANTIDADE");
            builder.Property(t => t.TransferId).HasColumnName("TRA_ID");
            builder.Property(t => t.MaterialId).HasColumnName("MAT_ID");
            builder.Property(t => t.STMMaterialId).HasColumnName("STA_ID");

            builder.HasOne(x => x.Transfer).WithMany(x => x.TransferMaterials).HasForeignKey(x => x.TransferId);
            builder.HasOne(x => x.Material).WithMany(x => x.TransferMaterials).HasForeignKey(x => x.MaterialId);
            builder.HasOne(x => x.STMMaterial).WithMany(x => x.TransferMaterials).HasForeignKey(x => x.STMMaterialId);
        }
    }
}