using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementMaterialMapping : IEntityTypeConfiguration<ReceivementMaterial>
    {
        public void Configure(EntityTypeBuilder<ReceivementMaterial> builder)
        {
            builder.ToTable("RECEBIMENTO_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("RCM_ID");
            builder.Property(x => x.Amount).HasColumnName("RCM_QUANTIDADE");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.OrderItemId).HasColumnName("PDM_ID");
            builder.Property(x => x.ReceivementInvoiceOrderId).HasColumnName("REC_ID");

            builder.HasOne(x => x.ReceivementInvoiceOrder).WithMany(x => x.ReceivementsMaterials).HasForeignKey(x => x.ReceivementInvoiceOrderId);
            builder.HasOne(x => x.Material).WithMany(x => x.ReceivementMaterials).HasForeignKey(x => x.MaterialId);
            builder.HasOne(x => x.OrderItem).WithMany(x => x.ReceivementMaterials).HasForeignKey(x => x.OrderItemId);
        }
    }
}