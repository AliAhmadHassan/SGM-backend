using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferAttachmentlMapping : IEntityTypeConfiguration<TransferAttachment>
    {
        public void Configure(EntityTypeBuilder<TransferAttachment> builder)
        {
            builder.ToTable("TRA_ANEXO");
            builder.Property(x => x.Id).HasColumnName("TAN_ID");
            builder.Property(x => x.Document).HasColumnName("TAN_DOCUMENTO");
            builder.Property(x => x.Fiscal).HasColumnName("TAN_FISCAL");
            builder.Property(x => x.TransferId).HasColumnName("TRA_ID");

            builder.HasOne(x => x.Transfer).WithMany(x => x.Attachments).HasForeignKey(x => x.TransferId);
        }
    }
}