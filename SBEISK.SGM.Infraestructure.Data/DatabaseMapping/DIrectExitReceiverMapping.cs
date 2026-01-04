using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DirectExitReceiverMapping : IEntityTypeConfiguration<DirectExitReceiver>
    {
        public void Configure(EntityTypeBuilder<DirectExitReceiver> builder)
        {
            builder.ToTable("SAIDA_DIRETA_DESTINATARIO");
            builder.Property(x => x.Id).HasColumnName("SAD_ID");
            builder.Property(x => x.DeliveryDate).HasColumnName("SAD_DT_RECEBIMENTO");
            builder.Property(x => x.Notes).HasColumnName("SAD_OBSERVACOES");
            builder.Property(x => x.ReceiverId).HasColumnName("DES_ID");
            builder.Property(x => x.UserDeliveryId).HasColumnName("USU_ID_ENTREGA");
            builder.Property(x => x.UserWithdrawId).HasColumnName("USU_ID_RETIRADA");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID_ORIGEM");

            builder.HasOne(x => x.Installation).WithMany().HasForeignKey(x => x.InstallationId);
            builder.HasOne(x => x.UserWithdraw).WithMany().HasForeignKey(x => x.UserWithdrawId);
            builder.HasOne(x => x.UserDelivery).WithMany().HasForeignKey(x => x.UserDeliveryId);
            builder.HasOne(x => x.Receiver).WithMany().HasForeignKey(x => x.ReceiverId);
            builder.HasMany(x => x.Emails).WithOne(x => x.DirectExitReceiver);
            builder.HasMany(x => x.Attachments).WithOne(x => x.DirectExitReceiver);
            builder.HasMany(x => x.DirectExitReceiverMaterials).WithOne(x => x.DirectExitReceiver);
            builder.HasMany(x => x.ReceivementDevolutionReceivers).WithOne(x => x.DirectExitReceiver);
        }
    }
}