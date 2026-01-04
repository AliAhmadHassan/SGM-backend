using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementAttachmentMapping : IEntityTypeConfiguration<ReceivementAttachment>
    {
        public void Configure(EntityTypeBuilder<ReceivementAttachment> builder)
        {
            builder.ToTable("REC_ANEXO");
            builder.Property(x => x.Id).HasColumnName("RCA_ID");
            builder.Property(x => x.Document).HasColumnName("RCA_DOCUMENTO");
            builder.Property(X => X.ReceivementInvoiceOrderId).HasColumnName("REC_ID");

            builder.HasOne(x => x.ReceivementInvoiceOrder).WithMany(x => x.ReceivementAttachments)
                .HasForeignKey(x => x.ReceivementInvoiceOrderId);
        }
    }
}