using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class STMAttachmentMapping : IEntityTypeConfiguration<STMAttachment>
    {
        public void Configure(EntityTypeBuilder<STMAttachment> builder)
        {
            builder.ToTable("STM_ANEXO");
            builder.Property(x => x.Id).HasColumnName("STA_ID");
            builder.Property(x => x.Document).HasColumnName("STA_DOCUMENTO");
            builder.Property(x => x.STMId).HasColumnName("STM_ID");

            builder.HasOne(x => x.STM).WithMany(x => x.Attachments).HasForeignKey(x => x.STMId);
        }
    }
}