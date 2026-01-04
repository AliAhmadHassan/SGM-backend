using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementDevolutionReceiverMapping : IEntityTypeConfiguration<ReceivementDevolutionReceiver>
    {
        public void Configure(EntityTypeBuilder<ReceivementDevolutionReceiver> builder)
        {
            builder.ToTable("REC_DEVOLUCAO_DESTINATARIO");
            builder.Property(x => x.Id).HasColumnName("RDD_ID");
            builder.Property(x => x.Notes).HasColumnName("RDD_OBSERVACOES");
            builder.Property(x => x.CreatedAt).HasColumnName("RDD_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("RDD_DT_ATUALIZACAO");
            builder.Property(x => x.ReceivementDate).HasColumnName("RDD_DT_RECEBIMENTO");
            builder.Property(x => x.IsDraft).HasColumnName("RDD_RASCUNHO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.UserResponsableId).HasColumnName("USU_ID_RESPONSAVEL");
            builder.Property(x => x.DirectExitReceiverId).HasColumnName("SAD_ID");

            builder.HasOne(x => x.UserResponsable).WithMany().HasForeignKey(x => x.UserResponsableId);
            builder.HasOne(x => x.DirectExitReceiver).WithMany(x => x.ReceivementDevolutionReceivers).HasForeignKey(x => x.DirectExitReceiverId);
            builder.HasMany(x => x.Emails).WithOne(x => x.ReceivementDevolutionReceiver);
            builder.HasMany(x => x.Attachments).WithOne(x => x.ReceivementDevolutionReceiver);
            builder.HasMany(x => x.DevolutionMaterialsReceiver).WithOne(x => x.ReceivementDevolutionReceiver);

            builder.UseUserModel();
        }
    }
}