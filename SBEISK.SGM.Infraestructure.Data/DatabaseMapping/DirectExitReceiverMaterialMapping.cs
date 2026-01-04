using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DirectExitReceiverMaterialMapping : IEntityTypeConfiguration<DirectExitReceiverMaterial>
    {
        public void Configure(EntityTypeBuilder<DirectExitReceiverMaterial> builder)
        {
            builder.ToTable("SAI_DEVOLUCAO_DESTINATARIO_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("DDM_ID");
            builder.Property(x => x.Amount).HasColumnName("DDM_QUANTIDADE");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.DirectExitReceiverId).HasColumnName("SAD_ID");
            builder.Property(x => x.DisciplineId).HasColumnName("DIS_ID");
            builder.Property(x => x.ReasonOfTemporaryCustodyId).HasColumnName("MGP_ID");

            builder.HasOne(x => x.Material).WithMany().HasForeignKey(x => x.MaterialId);
            builder.HasOne(x => x.DirectExitReceiver).WithMany(x => x.DirectExitReceiverMaterials).HasForeignKey(x => x.DirectExitReceiverId);
            builder.HasOne(x => x.Discipline).WithMany().HasForeignKey(x => x.DisciplineId);
        }
    }
}