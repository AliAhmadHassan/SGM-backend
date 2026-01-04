using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitReceiverAttachmentMapping : IEntityTypeConfiguration<ExitAttachment>
    {
        public void Configure(EntityTypeBuilder<ExitAttachment> builder)
        {
            builder.ToTable("SAI_DIR_ANEXO");
            builder.Property(x => x.Id).HasColumnName("SDN_ID");
            builder.Property(x => x.Document).HasColumnName("SDN_DOCUMENTO");
            builder.Property(x => x.Fiscal).HasColumnName("SDN_FISCAL");
            builder.Property(x => x.Authorization).HasColumnName("SDN_AUTORIZACAO");
            builder.Property(x => x.DirectExitId).HasColumnName("SDD_ID");

            builder.HasOne(x => x.DirectExit).WithMany(x => x.Attachments).HasForeignKey(x => x.DirectExitId);
        }
    }
}