using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementDevolutionMaterialMapping : IEntityTypeConfiguration<ReceivementDevolutionMaterial>
    {
        public void Configure(EntityTypeBuilder<ReceivementDevolutionMaterial> builder)
        {
            builder.ToTable("REC_DEVOLUCAO_DESTINATARIO_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("RDM_ID");
            builder.Property(x => x.Amount).HasColumnName("RDM_QUANTIDADE");
            builder.Property(x => x.AddtionalControl).HasColumnName("RDM_CONTROLE_ADICIONAL");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.ReceivementDevolutionReceiverId).HasColumnName("RDD_ID");
            builder.Property(x => x.MaterialStatusId).HasColumnName("MTS_ID");
            builder.Property(x => x.DevolutionReasonId).HasColumnName("MOD_ID");

            builder.HasOne(x => x.Material).WithMany().HasForeignKey(x => x.MaterialId);
            builder.HasOne(x => x.ReceivementDevolutionReceiver).WithMany(x => x.DevolutionMaterialsReceiver).HasForeignKey(x => x.ReceivementDevolutionReceiverId);
            builder.HasOne(x => x.MaterialStatus).WithMany().HasForeignKey(x => x.MaterialStatusId);
            builder.HasOne(x => x.DevolutionReason).WithMany(x => x.ReceivementDevolutionMaterials).HasForeignKey(x => x.DevolutionReasonId);
        }
    }
}