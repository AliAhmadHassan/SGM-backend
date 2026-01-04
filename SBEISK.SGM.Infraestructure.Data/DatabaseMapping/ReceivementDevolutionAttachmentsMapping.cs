using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementDevolutionAttachmentMapping : IEntityTypeConfiguration<ReceivementDevolutionAttachment>
    {
        public void Configure(EntityTypeBuilder<ReceivementDevolutionAttachment> builder)
        {
            builder.ToTable("REC_DEVOLUCAO_ANEXO");
            builder.Property(x => x.Id).HasColumnName("RDA_ID");
            builder.Property(x => x.Document).HasColumnName("RDA_DOCUMENTO");
            builder.Property(x => x.ReceivementDevolutionReceiverId).HasColumnName("RDD_ID");

            builder.HasOne(x => x.ReceivementDevolutionReceiver).WithMany().HasForeignKey(x => x.ReceivementDevolutionReceiverId);
        }
    }
}