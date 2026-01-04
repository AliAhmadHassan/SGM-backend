using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DirectExitReceiverAttachmentMapping : IEntityTypeConfiguration<DirectExitReceiverAttachment>
    {
        public void Configure(EntityTypeBuilder<DirectExitReceiverAttachment> builder)
        {
            builder.ToTable("SAI_DEVOLUCAO_DESTINATARIO_ANEXO");
            builder.Property(x => x.Id).HasColumnName("DDA_ID");
            builder.Property(x => x.Document).HasColumnName("DDA_DOCUMENTO");
            builder.Property(x => x.DirectExitReceiverId).HasColumnName("SAD_ID");

            builder.HasOne(x => x.DirectExitReceiver).WithMany().HasForeignKey(x => x.DirectExitReceiverId);
        }
    }
}